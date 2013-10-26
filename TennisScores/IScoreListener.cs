using TennisScores;

namespace APITests
{
    public interface IScoreListener
    {
        void OnScoreChanged(GameScoreDto gameScore);
    }
}