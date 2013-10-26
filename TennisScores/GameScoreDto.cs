namespace TennisScores
{
    public class GameScoreDto
    {
        public ScoreTypes Player1Score { get; private set; }
        public ScoreTypes Player2Score { get; private set; }

        public GameScoreDto(ScoreTypes score1, ScoreTypes score2)
        {
            Player1Score = score1;
            Player2Score = score2;
        }


    }
}
