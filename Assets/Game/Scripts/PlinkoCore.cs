using System;
using System.Collections.Generic;

namespace Game.Scripts
{
    public class PlinkoRoll
    {
        private readonly BallFallTurn[] _turns;
        
        public PlinkoRoll(int rowsCount)
        {
            _turns = new BallFallTurn[rowsCount + 1];
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
        
        private const int FIRST_ROW_PEGS_COUNT = 3;
        
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
            return random.Next(-1, 2);
        }
        
        
        public int GetPegsCount(int row)
        {
            return FIRST_ROW_PEGS_COUNT + (row - 1);
        }
    }
}