using System;
using APITests;

namespace TennisScores
{
    public class RandomGameSimulator : IScoreListener
    {
        private Game _game;

        public void PlayGame()
        {
            var rnd = new Random();
            _game = new Game(this);
            int playerNumberToWinPoint=0;

            while (_game.WinnerExists() == false)
            {
                playerNumberToWinPoint = rnd.Next(2);
                Console.WriteLine("player {0} wins a point", playerNumberToWinPoint+1);
                _game.WinPoint(playerNumberToWinPoint);
            }

            Console.WriteLine("Player {0} wins", playerNumberToWinPoint+1);
 
        }

        public void OnScoreChanged(GameScoreDto gameScoreDto)
        {
            if (_game.WinnerExists() == false)
            {
                if (gameScoreDto.Player1Score == ScoreTypes.Advantage)
                {
                    Console.WriteLine("Advantage Player 1");
                }
                else if (gameScoreDto.Player2Score == ScoreTypes.Advantage)
                {
                    Console.WriteLine("Advantage Player 2");
                }
                else if (gameScoreDto.Player1Score == ScoreTypes.Deuce || gameScoreDto.Player2Score == ScoreTypes.Deuce)
                {
                    Console.WriteLine("DEUCE!");
                }
                else
                {
                    Console.WriteLine("Score: {0} : {1}", gameScoreDto.Player1Score, gameScoreDto.Player2Score);
                }
            }
        }

    }
}
