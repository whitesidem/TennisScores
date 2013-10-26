using System;

namespace TennisScores
{
    class Program 
    {
        static void Main()
        {
            var gameSimulator = new RandomGameSimulator();
            gameSimulator.PlayGame();
            Console.ReadKey();
        }

    }
}
