namespace TennisScores
{
    public class GameScore
    {
        private int[] Points {  get;  set; }

        private readonly int[] _scoreValues = new[]{0, 15, 30, 40};

        public GameScore()
        {
            Points = new int[2];
        }

        public int PlayerScore(int playerId)
        {
            return _scoreValues[Points[playerId]];
        }


        public void AddPoint(int playerNumber)
        {
            Points[playerNumber] ++;
        }
    }
}