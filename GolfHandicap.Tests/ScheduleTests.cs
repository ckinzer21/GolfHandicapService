using GolfHandicap.Entities;
using GolfHandicap.Features.Matches.Post.GolfMatches.Scheduler;

namespace GolfHandicap.Tests
{
    [TestClass]
    public class ScheduleTests
    {
        private ScheduleYearlySchedule _class;

        [TestInitialize]
        public void Initialize()
        {
            _class = new ScheduleYearlySchedule();
        }

        // These aren't great tests, it's hard to test because of the random shuffle
        // There's probably a better way to test it, maybe injecting the random, but leaving as is for now
        // Keeping it for testing the class

        [TestMethod]
        public void ScheduleTheScheduleWithNoBlinds()
        {
            var matches = MatchSchedulesData();
            var golferIds = GolferIds();
            var expected = 30;

            var actual = _class.Schedule(matches, golferIds);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual.Count());            
        }

        [TestMethod]
        public void ScheduleTheScheduleWithBlinds()
        {
            var matches = MatchSchedulesBlindData();
            var golferIds = GolferIdsBlind();
            var expected = 35;

            var actual = _class.Schedule(matches, golferIds);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual.Count());
        }

        private List<MatchSchedule> MatchSchedulesBlindData()
        {
            var matches = new[]
            {
                new MatchSchedule { MatchScheduleId = 1, Week = 1, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 2,Week = 1, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 3,Week = 1, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 4,Week = 1, MatchDate = DateTime.Today, Blind = true},
                new MatchSchedule { MatchScheduleId = 5,Week = 2, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 6,Week = 2, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 7,Week = 2, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 8,Week = 2, MatchDate = DateTime.Today, Blind = true},
                new MatchSchedule { MatchScheduleId = 9,Week = 3, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 10,Week = 3, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 11,Week = 3, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 12,Week = 3, MatchDate = DateTime.Today, Blind = true},
                new MatchSchedule { MatchScheduleId = 13,Week = 4, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 14,Week = 4, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 15,Week = 4, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 16,Week = 4, MatchDate = DateTime.Today, Blind = true},
                new MatchSchedule { MatchScheduleId = 17,Week = 5, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 18,Week = 5, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 19,Week = 5, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 20,Week = 5, MatchDate = DateTime.Today, Blind = true},
            };

             return [..matches];
        }

        private List<int> GolferIdsBlind() => new() { 1, 2, 3, 4, 5, 6,7 };

        private List<MatchSchedule> MatchSchedulesData()
        {
            //Left gaps in ids to be sure it still works
            var matches = new[]
            {
                new MatchSchedule { MatchScheduleId = 1, Week = 1, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 2,Week = 1, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 3,Week = 1, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 5,Week = 2, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 6,Week = 2, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 7,Week = 2, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 9,Week = 3, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 10,Week = 3, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 11,Week = 3, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 13,Week = 4, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 14,Week = 4, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 15,Week = 4, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 17,Week = 5, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 18,Week = 5, MatchDate = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 19,Week = 5, MatchDate = DateTime.Today},
            };

            return [.. matches];
        }

        private List<int> GolferIds() => new() { 1, 2, 3, 4, 5, 6 };
    }
}