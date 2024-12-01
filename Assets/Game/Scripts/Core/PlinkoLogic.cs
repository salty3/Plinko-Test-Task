using Random = System.Random;

namespace Game.Scripts.Core
{
    public class PlinkoLogic
    {
        private readonly int _rowsCount;
        
        private const int FIRST_ROW_PINS_COUNT = 3;
        
        public PlinkoLogic(int rowsCount)
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
        
        
        public static int GetPinsCount(int row)
        {
            return FIRST_ROW_PINS_COUNT + (row - 1);
        }
    }
}