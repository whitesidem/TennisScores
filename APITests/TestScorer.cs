using NUnit.Framework;
using TennisScores;

namespace APITests
{
    [TestFixture]
    public class TestScorer : IScoreListener
    {
        private GameScoreDto _gameScoreDto;
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
            AssertScores(ScoreTypes.Fifteen, ScoreTypes.Love, "15 love");
        }


        [TestCase("Love All",ScoreTypes.Love, ScoreTypes.Love)]
//        [TestCase("15 Love", ScoreTypes.Fifteen, ScoreTypes.Love, Game.Player1)]
        [TestCase("15 15",ScoreTypes.Fifteen, ScoreTypes.Fifteen, Game.Player1, Game.Player2)]
        [TestCase("30 15",ScoreTypes.Thirty, ScoreTypes.Fifteen, Game.Player1, Game.Player2, Game.Player1)]
        [TestCase("30 30", ScoreTypes.Thirty, ScoreTypes.Thirty, Game.Player1, Game.Player2, Game.Player1, Game.Player2)]
        [TestCase("40 30", ScoreTypes.Fourty, ScoreTypes.Thirty, Game.Player1, Game.Player2, Game.Player1, Game.Player2, Game.Player1)]
        [TestCase("40 30 then player1 wins", ScoreTypes.Winner, ScoreTypes.Thirty, Game.Player1, Game.Player2, Game.Player1, Game.Player2, Game.Player1, Game.Player1)]
        [TestCase("Whitewash player1 win", ScoreTypes.Winner, ScoreTypes.Love, Game.Player1, Game.Player1, Game.Player1, Game.Player1)]
        [TestCase("Whitewash player2 win", ScoreTypes.Love, ScoreTypes.Winner, Game.Player2, Game.Player2, Game.Player2, Game.Player2)]
        [TestCase("Deuce", ScoreTypes.Deuce, ScoreTypes.Deuce, Game.Player1, Game.Player2, Game.Player1, Game.Player2, Game.Player1, Game.Player2)]
        [TestCase("Advantage Player1", ScoreTypes.Advantage, ScoreTypes.Deuce, Game.Player1, Game.Player2, Game.Player1, Game.Player2, Game.Player1, Game.Player2, Game.Player1)]
        [TestCase("Player 1 wins after Advantage", ScoreTypes.Winner, ScoreTypes.Deuce, Game.Player1, Game.Player2, Game.Player1, Game.Player2, Game.Player1, Game.Player2, Game.Player1, Game.Player1)]
        [TestCase("Player 1 Loses Advantage", ScoreTypes.Deuce, ScoreTypes.Deuce, Game.Player1, Game.Player2, Game.Player1, Game.Player2, Game.Player1, Game.Player2, Game.Player1, Game.Player2)]
        [TestCase("Advantage Player2", ScoreTypes.Deuce, ScoreTypes.Advantage, Game.Player1, Game.Player2, Game.Player1, Game.Player2, Game.Player1, Game.Player2, Game.Player2)]
        [TestCase("Player2 wins after advantage", ScoreTypes.Deuce, ScoreTypes.Winner, Game.Player1, Game.Player2, Game.Player1, Game.Player2, Game.Player1, Game.Player2, Game.Player2, Game.Player2)]
        [TestCase("Player2 Loses Advantage", ScoreTypes.Deuce, ScoreTypes.Deuce, Game.Player1, Game.Player2, Game.Player1, Game.Player2, Game.Player1, Game.Player2, Game.Player2, Game.Player1)]
        public void TestScores(string testName, ScoreTypes player1Score, ScoreTypes player2Score, params int[] playerWinsPoint)
        {
            //arrange
            //act
            foreach (var playerNumber in playerWinsPoint)
            {
                _game.WinPoint(playerNumber);                
            }

            //assert
            AssertScores(player1Score, player2Score, testName);
        }

        public void OnScoreChanged(GameScoreDto gameScore)
        {
            _gameScoreDto = gameScore;
        }

        private void AssertScores(ScoreTypes score1, ScoreTypes score2, string testName)
        {
            Assert.That(_gameScoreDto.Player1Score, Is.EqualTo(score1), "{0}: player 1 score not as expected", testName);
            Assert.That(_gameScoreDto.Player2Score, Is.EqualTo(score2), "{0}: player 2 score not as expected", testName);
        }


    }
}
