using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Game.Scripts
{
    public class PlinkoRoll
    {
        private readonly BallFallTurn[] _turns;
        
        public IEnumerable<BallFallTurn> Turns => _turns;
        
        public int ResultHole => _turns[^1].Position;
        
        public PlinkoRoll(int rowsCount)
        {
            _turns = new BallFallTurn[rowsCount];
        }
        
        public void AddTurn(int turnIndex, BallFallTurn turn)
        {
            _turns[turnIndex] = turn;
        }
    }

    public struct BallFallTurn
    {
        public int Position;
    }
    
    public class PlinkoCore
    {
        private int _rowsCount;
        
        private const int FIRST_ROW_PINS_COUNT = 3;
        
        public PlinkoCore(int rowsCount)
        {
            _rowsCount = rowsCount;
        }
        
        public PlinkoRoll CalculateRoll()
        {
            var plinkoRoll = new PlinkoRoll(_rowsCount);
            
            int currentPosition = 0;
            for (int i = 0; i < _rowsCount; i++)
            {
                currentPosition += GetPositionChange();
                plinkoRoll.AddTurn(i, new BallFallTurn
                {
                    Position = currentPosition
                });
            }
            
            return plinkoRoll;
        }
        
       
        
        private int GetPositionChange()
        {
            var random = new Random();
            var value = random.NextDouble();
            // We have ideal binomial distribution
            // I think any double position changes or another "errors" in one turn should be only animation effect
            return value < 0.5 ? -1 : 1;
        }
        
        
        public static int GetPegsCount(int row)
        {
            return FIRST_ROW_PINS_COUNT + (row - 1);
        }
    }
}