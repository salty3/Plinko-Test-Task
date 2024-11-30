using System;
using Tools.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class PinsRenderer : CachedMonoBehaviour
    {
        [SerializeField] private Transform _pinRendererPrefab;
        [SerializeField] private Transform _emptyPinRendererPrefab;

        [SerializeField] private GridLayoutGroup _boardGrid;


        private PlinkoCore _plinkoCore;
        
        
        public void RenderBoard(int rowsCount)
        {
            foreach (Transform tr in _boardGrid.transform)
            {
                Destroy(tr.gameObject);
            }
            int totalColumns = (rowsCount + 2) * 2 - 1;
            RecalculateCellSize(totalColumns);
            for (int row = 1; row <= rowsCount; row++)
            {
                var pinsInRow = PlinkoCore.GetPegsCount(row);

                for (int column = 0; column < totalColumns; column++)
                {
                    Instantiate(
                        ShouldPlacePeg(column, pinsInRow, totalColumns) ? _pinRendererPrefab : _emptyPinRendererPrefab,
                        _boardGrid.transform);
                }
            }
        }

        private void RecalculateCellSize(int totalColumns)
        {
            _boardGrid.constraintCount = totalColumns;
            var boardWidth = ((RectTransform) _boardGrid.transform).rect.width;
            var cellSize = boardWidth / totalColumns;
            _boardGrid.cellSize = new Vector2(cellSize, cellSize);
        }

        private bool ShouldPlacePeg(int column, int pinsInRow, int totalColumns)
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