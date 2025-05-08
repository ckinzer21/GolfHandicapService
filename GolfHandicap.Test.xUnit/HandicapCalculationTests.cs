using GolfHandicap.Common;
using GolfHandicap.Entities;
using GolfHandicap.Features.Scores.Handicaps.Calculation;
using Microsoft.Extensions.Options;

namespace GolfHandicap.Test.xUnit
{
    // Standard for testing in .NET
    // Tests are only for Blue tees
    public class HandicapCalculationTests
    {
        [Fact]
        public void CalculateHandicapIndexWhenLastSixScoresOnBaldwin()
        {
            var slopeSettings = new SlopeSettings { BaseSlope = 113 };
            var options = Options.Create(slopeSettings);
            var calculator = new HandicapCalculation(options);
            var expected = 2.68;

            var actual = calculator.CalculateHandicapIndex(FakePastSixScoresBaldwin(), Weighting());

            Assert.True(actual == expected);
        }

        [Fact]
        public void CalculateHandicapIndexWhenLastSixScoresOnFurnance()
        {
            var slopeSettings = new SlopeSettings { BaseSlope = 113 };
            var options = Options.Create(slopeSettings);
            var calculator = new HandicapCalculation(options);
            var expected = 3.61;

            var actual = calculator.CalculateHandicapIndex(FakePastSixScoresFurnance(), Weighting());

            Assert.True(actual == expected);
        }

        [Fact]
        public void CalculateHandicapIndexWhenLastSixScoresOnChippewa()
        {
            var slopeSettings = new SlopeSettings { BaseSlope = 113 };
            var options = Options.Create(slopeSettings);
            var calculator = new HandicapCalculation(options);
            var expected = 4.09;

            var actual = calculator.CalculateHandicapIndex(FakePastSixScoresChippewa(), Weighting());

            Assert.True(actual == expected);
        }

        [Fact]
        public void CalculateHandicapIndexWhenLastSixScoresOnMixedCourses()
        {
            var slopeSettings = new SlopeSettings { BaseSlope = 113 };
            var options = Options.Create(slopeSettings);
            var calculator = new HandicapCalculation(options);
            var expected = 3.46;

            var actual = calculator.CalculateHandicapIndex(FakePastSixScoresMixed(), Weighting());

            Assert.True(actual == expected);
        }

        private Weight Weighting() => new Weight { WeightId = 1, Value = 0.8 };
        
        public Course Baldwin()
        {
            return new Course
            {
                CourseId = 1,
                Name = "Baldwin",
                CourseRating = 37.4,
                Slope = 127,
                Tees = "Blue"
            };
        }

        public Course Furnance()
        {
            return new Course
            {
                CourseId = 2,
                Name = "Furnance",
                CourseRating = 36.1,
                Slope = 127,
                Tees = "Blue"
            };
        }

        public Course Chippewa()
        {
            return new Course
            {
                CourseId = 3,
                Name = "Chippewa",
                CourseRating = 35.6,
                Slope = 123,
                Tees = "Blue"
            };
        }

        public List<Score> FakePastSixScoresBaldwin()
        {
            return new List<Score>
            {
                new Score { ScoreId = 1, GolferId = 1, MatchScheduleId = 1, GrossStrokes = 44, AdjustedGrossStrokes = 42, Course = Baldwin()},
                new Score { ScoreId = 2, GolferId = 1, MatchScheduleId = 2, GrossStrokes = 42, AdjustedGrossStrokes = 42, Course = Baldwin()},
                new Score { ScoreId = 3, GolferId = 1, MatchScheduleId = 3, GrossStrokes = 48, AdjustedGrossStrokes = 41, Course = Baldwin()},
                new Score { ScoreId = 4, GolferId = 1, MatchScheduleId = 4, GrossStrokes = 40, AdjustedGrossStrokes = 36, Course = Baldwin()},
                new Score { ScoreId = 5, GolferId = 1, MatchScheduleId = 5, GrossStrokes = 50, AdjustedGrossStrokes = 44, Course = Baldwin()},
                new Score { ScoreId = 6, GolferId = 1, MatchScheduleId = 6, GrossStrokes = 44, AdjustedGrossStrokes = 42, Course = Baldwin()},
            };
        }

        public List<Score> FakePastSixScoresFurnance()
        {
            return new List<Score>
            {
                new Score { ScoreId = 1, GolferId = 1, MatchScheduleId = 1, GrossStrokes = 44, AdjustedGrossStrokes = 42, Course = Furnance()},
                new Score { ScoreId = 2, GolferId = 1, MatchScheduleId = 2, GrossStrokes = 42, AdjustedGrossStrokes = 42, Course = Furnance()},
                new Score { ScoreId = 3, GolferId = 1, MatchScheduleId = 3, GrossStrokes = 48, AdjustedGrossStrokes = 41, Course = Furnance()},
                new Score { ScoreId = 4, GolferId = 1, MatchScheduleId = 4, GrossStrokes = 40, AdjustedGrossStrokes = 36, Course = Furnance()},
                new Score { ScoreId = 5, GolferId = 1, MatchScheduleId = 5, GrossStrokes = 50, AdjustedGrossStrokes = 44, Course = Furnance()},
                new Score { ScoreId = 6, GolferId = 1, MatchScheduleId = 6, GrossStrokes = 44, AdjustedGrossStrokes = 42, Course = Furnance()},
            };
        }

        public List<Score> FakePastSixScoresChippewa()
        {
            return new List<Score>
            {
                new Score { ScoreId = 1, GolferId = 1, MatchScheduleId = 1, GrossStrokes = 44, AdjustedGrossStrokes = 42, Course = Chippewa()},
                new Score { ScoreId = 2, GolferId = 1, MatchScheduleId = 2, GrossStrokes = 42, AdjustedGrossStrokes = 42, Course = Chippewa()},
                new Score { ScoreId = 3, GolferId = 1, MatchScheduleId = 3, GrossStrokes = 48, AdjustedGrossStrokes = 41, Course = Chippewa()},
                new Score { ScoreId = 4, GolferId = 1, MatchScheduleId = 4, GrossStrokes = 40, AdjustedGrossStrokes = 36, Course = Chippewa()},
                new Score { ScoreId = 5, GolferId = 1, MatchScheduleId = 5, GrossStrokes = 50, AdjustedGrossStrokes = 44, Course = Chippewa()},
                new Score { ScoreId = 6, GolferId = 1, MatchScheduleId = 6, GrossStrokes = 44, AdjustedGrossStrokes = 42, Course = Chippewa()},
            };
        }

        public List<Score> FakePastSixScoresMixed()
        {
            return new List<Score>
            {
                new Score { ScoreId = 1, GolferId = 1, MatchScheduleId = 1, GrossStrokes = 44, AdjustedGrossStrokes = 42, Course = Baldwin()},
                new Score { ScoreId = 2, GolferId = 1, MatchScheduleId = 2, GrossStrokes = 42, AdjustedGrossStrokes = 42, Course = Furnance()},
                new Score { ScoreId = 3, GolferId = 1, MatchScheduleId = 3, GrossStrokes = 48, AdjustedGrossStrokes = 41, Course = Chippewa()},
                new Score { ScoreId = 4, GolferId = 1, MatchScheduleId = 4, GrossStrokes = 40, AdjustedGrossStrokes = 36, Course = Baldwin()},
                new Score { ScoreId = 5, GolferId = 1, MatchScheduleId = 5, GrossStrokes = 50, AdjustedGrossStrokes = 44, Course = Furnance()},
                new Score { ScoreId = 6, GolferId = 1, MatchScheduleId = 6, GrossStrokes = 44, AdjustedGrossStrokes = 42, Course = Chippewa()},
            };
        }
    }
}