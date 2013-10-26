using NUnit.Framework;
using TennisScores;

namespace APITests
{
    [TestFixture]
    public class TestScorer : IScoreListener
    {
        private GameScore _gameScore;
        private Game _game;

        [SetUp]
        public void SetUp()
        {
            _game = new Game(this);
            _game.NewGame();
        }

        [Test]
        public void FirstPlayerFirstPointScore_Is_15_0()
        {
            //arrange
            //act
            _game.WinPoint(Game.Player1);

            //assert
            AssertScores(ScoreTypes.Fifteen, ScoreTypes.Love);
        }

        [TestCase(ScoreTypes.Love, ScoreTypes.Love)]
//        [TestCase(ScoreTypes.Fifteen, ScoreTypes.Love, Game.Player1)]
        [TestCase(ScoreTypes.Fifteen, ScoreTypes.Fifteen, Game.Player1, Game.Player2)]
        [TestCase(ScoreTypes.Thirty, ScoreTypes.Fifteen, Game.Player1, Game.Player2, Game.Player1)]
        [TestCase(ScoreTypes.Thirty, ScoreTypes.Thirty, Game.Player1, Game.Player2, Game.Player1, Game.Player2)]
        [TestCase(ScoreTypes.Fourty, ScoreTypes.Thirty, Game.Player1, Game.Player2, Game.Player1, Game.Player2, Game.Player1)]
        [TestCase(ScoreTypes.Deuce, ScoreTypes.Deuce, Game.Player1, Game.Player2, Game.Player1, Game.Player2, Game.Player1, Game.Player2)]
        [TestCase(ScoreTypes.Advantage, ScoreTypes.Deuce, Game.Player1, Game.Player2, Game.Player1, Game.Player2, Game.Player1, Game.Player2, Game.Player1)]
        [TestCase(ScoreTypes.Deuce, ScoreTypes.Deuce, Game.Player1, Game.Player2, Game.Player1, Game.Player2, Game.Player1, Game.Player2, Game.Player1, Game.Player2)]
        public void TestScores(ScoreTypes player1Score, ScoreTypes player2Score, params int[] playerWinsPoint)
        {
            //arrange
            //act
            foreach (var playerNumber in playerWinsPoint)
            {
                _game.WinPoint(playerNumber);                
            }

            //assert
            AssertScores(player1Score, player2Score);
        }

        public void OnScoreChanged(GameScore gameScore)
        {
            _gameScore = gameScore;
        }

        private void AssertScores(ScoreTypes score1, ScoreTypes score2)
        {
            Assert.That(_gameScore.PlayerScore(Game.Player1), Is.EqualTo(score1), "player 1 score not as expected");
            Assert.That(_gameScore.PlayerScore(Game.Player2), Is.EqualTo(score2), "player 2 score not as expected");
        }


    }
}
