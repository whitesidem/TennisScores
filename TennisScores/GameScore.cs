namespace TennisScores
{
    public class GameScore
    {
        private int[] Points {  get;  set; }

        private readonly ScoreTypes[] _scoreValues = new[] { ScoreTypes.Love, ScoreTypes.Fifteen, ScoreTypes.Thirty, ScoreTypes.Fourty, ScoreTypes.Deuce, ScoreTypes.Advantage };

        public GameScore()
        {
            Points = new int[2];
        }

        public ScoreTypes PlayerScore(int playerId)
        {
            return _scoreValues[Points[playerId]];
        }


        public void AddPoint(int playerNumber)
        {
            Points[playerNumber] ++;
            AdjustPointsForDeuceSenario();
        }

        private void AdjustPointsForDeuceSenario()
        {
            if (PlayerScore(Game.Player1) == ScoreTypes.Fourty && PlayerScore(Game.Player2) == ScoreTypes.Fourty)
            {
                Points[Game.Player1]++;
                Points[Game.Player2]++;
            }
        }
    }
}