using GolfHandicap.Entities;
using GolfHandicap.Features.Matches.Post.GolfMatches.Scheduler;
using GolfHandicap.Features.Matches.Post.Schedules;

namespace GolfHandicap.Test.xUnit
{
    public class GenerateMatchScheduleTests
    {
        [Fact]
        public void CreateScheduleThirteen()
        {
            var request = ScheduleRequest();
            var golfers = GolfersThirteen();
            GenerateMatchSchedule gen = new GenerateMatchSchedule();
            var expected = 95;
            var expectedDates = ExpectedDates();

            var actual = gen.Generate(request, golfers);
            
            Assert.True(actual.ToList().Count == expected);

            foreach (var date in expectedDates)
            {
                Assert.True(actual.ToList().Any(d => d.MatchDate == date));
            }
        }

        [Fact]
        public void CreateScheduleFourteen()
        {
            var request = ScheduleRequest();
            var golfers = GolfersFourteen();
            GenerateMatchSchedule gen = new GenerateMatchSchedule();
            var expected = 95;
            var expectedDates = ExpectedDates();

            var actual = gen.Generate(request, golfers);

            Assert.True(actual.ToList().Count == expected);

            foreach (var date in expectedDates)
            {
                Assert.True(actual.ToList().Any(d => d.MatchDate == date));
            }
        }

        [Fact]
        public void CreateScheduleSix()
        {
            var request = ScheduleRequest();
            var golfers = GolfersSix();
            GenerateMatchSchedule gen = new GenerateMatchSchedule();
            var expected = 19;
            var expectedDates = ExpectedDatesSix();

            var actual = gen.Generate(request, golfers);

            Assert.True(actual.ToList().Count == expected);

            foreach(var date in expectedDates)
            {
                Assert.True(actual.ToList().Any(d => d.MatchDate == date));
            }
        }

        private List<Golfer> GolfersFourteen()
        {
            return new()
            {
                new Golfer {GolferId = 1, Name = "A", FlightId = 2},
                new Golfer {GolferId = 2, Name = "B", FlightId = 2},
                new Golfer {GolferId = 3, Name = "C", FlightId = 2},
                new Golfer {GolferId = 4, Name = "D", FlightId = 2},
                new Golfer {GolferId = 5, Name = "E", FlightId = 2},
                new Golfer {GolferId = 6, Name = "F", FlightId = 2},
                new Golfer {GolferId = 7, Name = "G", FlightId = 2},
                new Golfer {GolferId = 8, Name = "H", FlightId = 2},
                new Golfer {GolferId = 9, Name = "I", FlightId = 2},
                new Golfer {GolferId = 10, Name = "J", FlightId = 2},
                new Golfer {GolferId = 11, Name = "K", FlightId = 2},
                new Golfer {GolferId = 12, Name = "L", FlightId = 2},
                new Golfer {GolferId = 13, Name = "M", FlightId = 2},
                new Golfer {GolferId = 14, Name = "N", FlightId = 2},
            };
        }

        private List<Golfer> GolfersThirteen()
        {
            return new()
            {
                new Golfer {GolferId = 1, Name = "A", FlightId = 1},
                new Golfer {GolferId = 2, Name = "B", FlightId = 1},
                new Golfer {GolferId = 3, Name = "C", FlightId = 1},
                new Golfer {GolferId = 4, Name = "D", FlightId = 1},
                new Golfer {GolferId = 5, Name = "E", FlightId = 1},
                new Golfer {GolferId = 6, Name = "F", FlightId = 1},
                new Golfer {GolferId = 7, Name = "G", FlightId = 1},
                new Golfer {GolferId = 8, Name = "H", FlightId = 1},
                new Golfer {GolferId = 9, Name = "I", FlightId = 1},
                new Golfer {GolferId = 10, Name = "J", FlightId = 1},
                new Golfer {GolferId = 11, Name = "K", FlightId = 1},
                new Golfer {GolferId = 12, Name = "L", FlightId = 1},
                new Golfer {GolferId = 13, Name = "M", FlightId = 1},
            };
        }

        private List<Golfer> GolfersSix()
        {
            return new()
            {
                new Golfer {GolferId = 1, Name = "A", FlightId = 1},
                new Golfer {GolferId = 2, Name = "B", FlightId = 1},
                new Golfer {GolferId = 3, Name = "C", FlightId = 1},
                new Golfer {GolferId = 4, Name = "D", FlightId = 1},
                new Golfer {GolferId = 5, Name = "E", FlightId = 1},
                new Golfer {GolferId = 6, Name = "F", FlightId = 1},
            };
        }

        private ScheduleRequest ScheduleRequest()
        {
            return new ScheduleRequest(new DateTime(2025, 5, 6), MajorScheduleRequest(), 1);
        }

        private List<MajorScheduleRequest> MajorScheduleRequest()
        {
            return new()
            {
                new MajorScheduleRequest { Date = new DateTime(2025,5,6), MajorId = 1},
                new MajorScheduleRequest { Date = new DateTime(2025,6,10), MajorId = 2},
                new MajorScheduleRequest { Date = new DateTime(2025,7,15), MajorId = 3},
                new MajorScheduleRequest { Date = new DateTime(2025,8,26), MajorId = 4}
            };
        }

        private List<DateTime> ExpectedDates()
        {
            return new()
            {
                new DateTime(2025,5,6),
                new DateTime(2025,5,13),
                new DateTime(2025,5,20),
                new DateTime(2025,5,27),
                new DateTime(2025,6,3),
                new DateTime(2025,6,10),
                new DateTime(2025,6,17),
                new DateTime(2025,6,24),
                new DateTime(2025,7,1),
                new DateTime(2025,7,8),
                new DateTime(2025,7,15),
                new DateTime(2025,7,22),
                new DateTime(2025,7,29),
                new DateTime(2025,8,5),
                new DateTime(2025,8,12),
                new DateTime(2025,8,19),
                new DateTime(2025,8,26),
            };
        }

        private List<DateTime> ExpectedDatesSix()
        {
            return new()
            {
                new DateTime(2025,5,6),
                new DateTime(2025,5,13),
                new DateTime(2025,5,20),
                new DateTime(2025,5,27),
                new DateTime(2025,6,3),
                new DateTime(2025,6,10),
                new DateTime(2025,6,17),
                new DateTime(2025,7,15),
                new DateTime(2025,8,26),
            };
        }
    }
}
