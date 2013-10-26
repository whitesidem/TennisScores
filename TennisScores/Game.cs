using APITests;

namespace TennisScores
{
    public class Game
    {
        private GameScore _gameScore;
        private readonly IScoreListener _scoreListener;
        public const int Player1 = 0;
        public const int Player2 = 1;

        public Game(IScoreListener scoreListener)
        {
            _scoreListener = scoreListener;
        }

        public void NewGame()
        {
            _gameScore = new GameScore();
            NotifyScoreChanged();
        }

        private void NotifyScoreChanged()
        {
            _scoreListener.OnScoreChanged(_gameScore);
        }

        public void WinPoint(int playerNumber)
        {
            _gameScore.AddPoint(playerNumber);
        }


    }
}