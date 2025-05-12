using GolfHandicap.Entities;

namespace GolfHandicap.Common.Handicaps
{
    public interface IHandicapCalculation
    {
        double CalculateHandicapIndex(List<Score> scores, Weight weight, int? takeLowestNAmountOfScores = null);
    }
}
