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
            AssertScores(15, 0);
        }

        [TestCase(0, 0)]
        [TestCase(15, 15, Game.Player1, Game.Player2)]
        [TestCase(30, 15, Game.Player1, Game.Player2, Game.Player1)]
        [TestCase(30, 30, Game.Player1, Game.Player2, Game.Player1, Game.Player2)]
        [TestCase(40, 30, Game.Player1, Game.Player2, Game.Player1, Game.Player2, Game.Player1)]
        public void TestScores(int player1Score, int player2Score, params int[] playerWinsPoint)
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

        private void AssertScores(int score1, int score2)
        {
            Assert.That(_gameScore.PlayerScore(Game.Player1), Is.EqualTo(score1), "player 1 score not as expected");
            Assert.That(_gameScore.PlayerScore(Game.Player2), Is.EqualTo(score2), "player 2 score not as expected");
        }


    }
}
