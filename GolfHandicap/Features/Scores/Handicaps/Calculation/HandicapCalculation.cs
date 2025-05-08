using GolfHandicap.Common;
using GolfHandicap.Entities;
using Microsoft.Extensions.Options;

namespace GolfHandicap.Features.Scores.Handicaps.Calculation
{

    public class HandicapCalculation : IHandicapCalculation
    {
        private readonly SlopeSettings _slopeSettings;

        public HandicapCalculation(IOptions<SlopeSettings> options)
        {
            _slopeSettings = options.Value;
        }

        // league uses past 6 rounds at 80%
        // StartOfYear uses best 10 of last 20 rounds at 80%
        // Handicap = (Score - Course Rating) * 113 / Slope Rating
        public double CalculateHandicapIndex(List<Score> scores, Weight weight)
        {
            var diffs = new List<double>();

            foreach (var score in scores)
            {
                var diff = Math.Round((score.AdjustedGrossStrokes - score.Tee.CourseRating) * _slopeSettings.BaseSlope / score.Tee.Slope, 2, MidpointRounding.AwayFromZero);
                diffs.Add(diff);
            }

            return Math.Round(diffs.Average() * weight.Value, 2, MidpointRounding.AwayFromZero);
        }
    }
}
