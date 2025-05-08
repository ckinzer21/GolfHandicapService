using GolfHandicap.Entities;

namespace GolfHandicap.Features.Scores.Handicaps.Calculation
{
    public interface IHandicapCalculation
    {
        double CalculateHandicapIndex(List<Score> scores, Weight weight);
    }
}
