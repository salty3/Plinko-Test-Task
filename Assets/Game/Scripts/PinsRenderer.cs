using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Tools.Runtime;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Game.Scripts
{
    public class PinsRenderer : CachedMonoBehaviour
    {
        [SerializeField] private Transform _pinRendererPrefab;
        [SerializeField] private Transform _emptyPinRendererPrefab;
        [SerializeField] private Transform _ballRendererPrefab;

        
        [SerializeField] private RectTransform _ballRenderer;
        [SerializeField] private GridLayoutGroup _boardGrid;

        private PlinkoCore _plinkoCore;
        
        
        public void RenderBoard(int rowsCount)
        {
            foreach (Transform tr in _boardGrid.transform)
            {
                Destroy(tr.gameObject);
            }
            int totalColumns = (rowsCount + 2) * 2 - 1;
            RecalculateCellSize(rowsCount, totalColumns);
            for (int row = 1; row <= rowsCount; row++)
            {
                var pinsInRow = PlinkoCore.GetPegsCount(row);

                for (int column = 0; column < totalColumns; column++)
                {
                    Instantiate(
                        ShouldPlacePin(column, pinsInRow, totalColumns) ? _pinRendererPrefab : _emptyPinRendererPrefab,
                        _boardGrid.transform);
                }
            }
        }

        public async UniTask PlayRollAnimation(PlinkoRoll roll)
        {
            
            var ball = (RectTransform)Instantiate(_ballRendererPrefab, _ballRenderer);
            
            //set pos at center top
            ball.anchoredPosition = new Vector2(0, _ballRenderer.rect.height / 2);
            //set size
            ball.sizeDelta = new Vector2(_boardGrid.cellSize.x, _boardGrid.cellSize.x) * 1.6f;
            
            var turnsArray = roll.Turns.ToArray();
            for (var index = 0; index < turnsArray.Length; index++)
            {
                var ballFallTurn = turnsArray[index];
                await AnimateOneFallTurn(ball, index, ballFallTurn);
            }
        }
        
        private static float GetRandomNumber(float minimum, float maximum)
        { 
            return UnityEngine.Random.value * (maximum - minimum) + minimum;
        }
        
        private async UniTask AnimateOneFallTurn(RectTransform ball, int turnIndex, BallFallTurn ballFallTurn)
        {
            ball.DOShakeAnchorPos(GetRandomNumber(0.05f, 0.20f), GetRandomNumber(4f, 10f), (int)GetRandomNumber(35f, 70f));
            var targetPosition = GridPositionToLocalPosition(ballFallTurn.Position, turnIndex + 1);
            await ball.DOAnchorPos(targetPosition, GetRandomNumber(0.15f, 0.4f)).SetEase(Ease.OutBounce);
        }
        
        private Vector2 GridPositionToLocalPosition(int x, int y)
        {
            return new Vector2(x * _boardGrid.cellSize.x, -y * (_boardGrid.cellSize.y + _boardGrid.spacing.y) + _ballRenderer.rect.height / 2);
        }

        private void RecalculateCellSize(int rowsCount, int totalColumns)
        {
            _boardGrid.constraintCount = totalColumns;
            var boardWidth = ((RectTransform) _boardGrid.transform).rect.width;
            var cellSize = boardWidth / totalColumns;
            _boardGrid.cellSize = new Vector2(cellSize, cellSize);
            _ballRenderer.sizeDelta = new Vector2(cellSize * totalColumns, (cellSize + _boardGrid.spacing.y) * rowsCount);
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