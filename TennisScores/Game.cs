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
            _gameScore = new GameScore();
            _scoreListener = scoreListener;
        }

        public void NewGame()
        {
            _gameScore = new GameScore();
            NotifyScoreChanged();
        }

        private void NotifyScoreChanged()
        {
            var gameScoreDto = new GameScoreDto(_gameScore.PlayerScore(Player1),
                                                _gameScore.PlayerScore(Player2));
            _scoreListener.OnScoreChanged(gameScoreDto);
        }

        public void WinPoint(int playerNumber)
        {
            _gameScore.AddPoint(playerNumber);
            NotifyScoreChanged();
        }

        public bool WinnerExists()
        {
            return _gameScore.WinnerExists();
        }

    }
}