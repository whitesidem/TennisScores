using TennisScores;

namespace APITests
{
    public interface IScoreListener
    {
        void OnScoreChanged(GameScore gameScore);
    }
}