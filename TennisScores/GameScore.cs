using System;

namespace TennisScores
{
    public class GameScore
    {
        private int[] Points {  get;  set; }

        private readonly ScoreTypes[] _scoreValues = new[] { ScoreTypes.Love, ScoreTypes.Fifteen, ScoreTypes.Thirty, ScoreTypes.Fourty, ScoreTypes.Winner, ScoreTypes.Deuce, ScoreTypes.Advantage };

        public GameScore()
        {
            Points = new int[2];
        }

        public ScoreTypes PlayerScore(int playerId)
        {
            return _scoreValues[Points[playerId]];
        }

        internal void AddPoint(int playerNumber)
        {
            if (AnyPlayerHasAdvantage())
            {
                ApplyAdvantageRuleForNewPoint(playerNumber);
            }
            else
            {
                ApplyStandardRuleForNewPoint(playerNumber);
            }
        }

        private void ApplyStandardRuleForNewPoint(int playerNumber)
        {
            IncrementPoint(playerNumber);
            AdjustPointsForDeuceSenario();
        }

        private void ApplyAdvantageRuleForNewPoint(int playerNumberScoringPoint)
        {
            if(PlayerHasAdvantage(playerNumberScoringPoint))
            {
                SetPlayerAsWinner(playerNumberScoringPoint);
            }
            else
            {
                SetPlayerToDeuce(playerNumberScoringPoint);
            }
        }

        private bool AnyPlayerHasAdvantage()
        {
            return (PlayerHasAdvantage(Game.Player1) || PlayerHasAdvantage(Game.Player2));
        }

        private bool PlayerHasAdvantage(int playerNumber)
        {
            return PlayerScore(playerNumber) == ScoreTypes.Advantage;
        }

        private bool PlayerHasWon(int playerNumber)
        {
            return PlayerScore(playerNumber) == ScoreTypes.Winner;
        }

        internal bool WinnerExists()
        {
            return (PlayerHasWon(Game.Player1) || PlayerHasWon(Game.Player2));
        }

        private void IncrementPoint(int playerNumber)
        {
            Points[playerNumber]++;
        }

        private void AdjustPointsForDeuceSenario()
        {
            if (QualifiesForDeuce())
            {
                SetPlayerToDeuce(Game.Player1);
                SetPlayerToDeuce(Game.Player2);
            }
        }

        private bool QualifiesForDeuce()
        {
            return PlayerOn40(Game.Player1) && PlayerOn40(Game.Player2);
        }

        private bool PlayerOn40(int playerNumber)
        {
            return PlayerScore(playerNumber) == ScoreTypes.Fourty;
        }

        private void SetPlayerToDeuce(int playerNumberScoringPoint)
        {
            Points[GetOpponent(playerNumberScoringPoint)] = ((int)ScoreTypes.Deuce);
        }

        private int GetOpponent(int playerNumber)
        {
            return playerNumber == Game.Player1 ? Game.Player2 : Game.Player1;
        }

        private void SetPlayerAsWinner(int playerNumberScoringPoint)
        {
            Points[playerNumberScoringPoint] = ((int)ScoreTypes.Winner);
        }

    }
}