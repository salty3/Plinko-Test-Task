namespace Game.Scripts.Core
{
    public class PlinkoRoll
    {
        private readonly BallFallTurn[] _turns;

        public int ResultHole => _turns[^1].Position;
        
        public int TurnsCount => _turns.Length;

        public BallFallTurn this[int index] => _turns[index];

        public PlinkoRoll(int rowsCount)
        {
            _turns = new BallFallTurn[rowsCount];
        }

        public void AddTurn(int turnIndex, BallFallTurn turn)
        {
            _turns[turnIndex] = turn;
        }
    }
}