using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Game.PoolingSystem;
using Game.Scripts.BetSystem;
using Game.Scripts.Core;
using Tools.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class PinsRenderer : UIMonoBehaviour
    {
        [SerializeField] private Transform _pinRendererPrefab;
        [SerializeField] private Transform _emptyPinRendererPrefab;
        [Space]
        [SerializeField] private Transform _lowRiskBallRendererPrefab;
        [SerializeField] private Transform _mediumRiskBallRendererPrefab;
        [SerializeField] private Transform _highRiskBallRendererPrefab;
        [Space]
        [SerializeField] private Transform _lowRiskBoxPrefab;
        [SerializeField] private Transform _mediumRiskBoxPrefab;
        [SerializeField] private Transform _highRiskBoxPrefab;
        [Space]
        [SerializeField] private RectTransform _lowRiskBoxContainer;
        [SerializeField] private RectTransform _mediumRiskBoxContainer;
        [SerializeField] private RectTransform _highRiskBoxContainer;
        [Space]
        [SerializeField] private RectTransform _ballRendererPanel;
        [SerializeField] private GridLayoutGroup _boardGrid;
        
        private Dictionary<BetRisk, Pool> _ballPools;

        private PlinkoLogic _plinkoLogic;

        private const int DEFAULT_POOL_SIZE = 30;

        private readonly List<BetMultiplierBoxView> _lowBoxes = new();
        private readonly List<BetMultiplierBoxView> _mediumBoxes = new();
        private readonly List<BetMultiplierBoxView> _highBoxes = new();
        
        protected override void Awake()
        {
            _ballPools = new Dictionary<BetRisk, Pool>
            {
                {BetRisk.Low, new Pool(_lowRiskBallRendererPrefab.gameObject, DEFAULT_POOL_SIZE)},
                {BetRisk.Medium, new Pool(_mediumRiskBallRendererPrefab.gameObject, DEFAULT_POOL_SIZE)},
                {BetRisk.High, new Pool(_highRiskBallRendererPrefab.gameObject, DEFAULT_POOL_SIZE)}
            };
        }

        
        public void RenderAll(int rowsCount, WinValuesConfig config)
        {
            RenderBoard(rowsCount);
            RenderBoxes(config, rowsCount);
        }

        private void RenderBoard(int rowsCount)
        {
            foreach (Transform tr in _boardGrid.transform)
            {
                Destroy(tr.gameObject);
            }
            int totalColumns = (rowsCount + 2) * 2 - 1;
            RecalculateCellSize(rowsCount, totalColumns);
            for (int row = 1; row <= rowsCount; row++)
            {
                var pinsInRow = PlinkoLogic.GetPinsCount(row);

                for (int column = 0; column < totalColumns; column++)
                {
                    Instantiate(
                        ShouldPlacePin(column, pinsInRow, totalColumns) ? _pinRendererPrefab : _emptyPinRendererPrefab,
                        _boardGrid.transform);
                }
            }
        }

        private void RenderBoxes(WinValuesConfig config, int rowsCount)
        {
            RenderBoxesForRisk(_lowRiskBoxContainer, _lowRiskBoxPrefab, BetRisk.Low, config, rowsCount, _lowBoxes);
            RenderBoxesForRisk(_mediumRiskBoxContainer, _mediumRiskBoxPrefab, BetRisk.Medium, config, rowsCount, _mediumBoxes);
            RenderBoxesForRisk(_highRiskBoxContainer, _highRiskBoxPrefab, BetRisk.High, config, rowsCount, _highBoxes);
        }
        
        private void RenderBoxesForRisk(Transform container, Transform prefab, BetRisk risk, WinValuesConfig config, int rowsCount, List<BetMultiplierBoxView> boxes)
        {
            foreach (Transform tr in container)
            {
                boxes.Clear();
                Destroy(tr.gameObject);
            }

            for (int i = 0; i < rowsCount + 1; i++)
            {
                var index = i - rowsCount / 2;
                var multiplier = Convert.ToDecimal(config.GetMultiplier(risk, index));
                var box = Instantiate(prefab, container).GetComponent<BetMultiplierBoxView>();
                box.SetBetMultiplier(multiplier);
                boxes.Add(box);
            }
        }
        
        private List<BetMultiplierBoxView> GetBoxesForRisk(BetRisk risk)
        {
            return risk switch
            {
                BetRisk.Low => _lowBoxes,
                BetRisk.Medium => _mediumBoxes,
                BetRisk.High => _highBoxes,
                _ => throw new ArgumentOutOfRangeException(nameof(risk), risk, null)
            };
        }

        public async UniTask PlayRollAnimation(BetRisk risk, PlinkoRoll roll)
        {
            var ball = (RectTransform) _ballPools[risk].Get().transform;
            ball.SetParent(_ballRendererPanel);
            ball.anchoredPosition = new Vector2(0, _ballRendererPanel.rect.height / 2);
            ball.sizeDelta = new Vector2(_boardGrid.cellSize.x, _boardGrid.cellSize.x) * 1.6f;
        
            for (var index = 0; index < roll.TurnsCount; index++)
            {
                var ballFallTurn = roll[index];
                await AnimateOneFallTurn(ball, index, ballFallTurn);
            }

            var boxes = GetBoxesForRisk(risk);

            var i = (int)MathTool.RemapRange(roll.ResultHole, -roll.TurnsCount, roll.TurnsCount, 0, boxes.Count - 1);
            boxes[i].PlayAnimation();
            
            _ballPools[risk].Return(ball.gameObject);
        }
        
        private async UniTask AnimateOneFallTurn(RectTransform ball, int turnIndex, BallFallTurn ballFallTurn)
        {
            ball.DOShakeAnchorPos(UnityEngine.Random.Range(0.05f, 0.20f), UnityEngine.Random.Range(4f, 10f), UnityEngine.Random.Range(35, 71));
            var targetPosition = GridPositionToLocalPosition(ballFallTurn.Position, turnIndex + 1);
            await ball.DOAnchorPos(targetPosition, UnityEngine.Random.Range(0.15f, 0.4f)).SetEase(Ease.OutBounce);
        }
        
        private Vector2 GridPositionToLocalPosition(int x, int y)
        {
            return new Vector2(x * _boardGrid.cellSize.x, -y * (_boardGrid.cellSize.y + _boardGrid.spacing.y) + _ballRendererPanel.rect.height / 2);
        }

        private void RecalculateCellSize(int rowsCount, int totalColumns)
        {
            _boardGrid.constraintCount = totalColumns;
            var boardTransform = RectTransform;
            var boardWidth = boardTransform.rect.width;
            var cellSize = boardWidth / totalColumns;
            _boardGrid.cellSize = new Vector2(cellSize, cellSize);
            boardTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, (cellSize + _boardGrid.spacing.y) * rowsCount);
            _ballRendererPanel.sizeDelta = new Vector2(boardWidth, (cellSize + _boardGrid.spacing.y) * rowsCount);
            _ballRendererPanel.localPosition = boardTransform.localPosition;
            
            _lowRiskBoxContainer.sizeDelta = new Vector2(cellSize * totalColumns, cellSize * 2);
            _mediumRiskBoxContainer.sizeDelta = new Vector2(cellSize * totalColumns, cellSize * 2);
            _highRiskBoxContainer.sizeDelta = new Vector2(cellSize * totalColumns, cellSize * 2);
        }

        private bool ShouldPlacePin(int column, int pinsInRow, int totalColumns)
        {
            int centerColumn = totalColumns / 2;
            int distanceFromCenter = Math.Abs(centerColumn - column);
            if (pinsInRow % 2 == 0)
            {
                return distanceFromCenter % 2 != 0 && distanceFromCenter < pinsInRow && centerColumn != column;
            }

            return distanceFromCenter % 2 == 0 && distanceFromCenter < pinsInRow;
        }
    }
}