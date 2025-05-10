using GolfHandicap.Entities;
using GolfHandicap.Features.Matches.Post.GolfMatches.Scheduler;

namespace GolfHandicap.Test.xUnit
{
    public class ScheduleYearlyScheduleTests
    {
        [Fact]
        public void ScheduleTheScheduleWithNoBlinds()
        {
            var matches = MatchSchedulesData();
            var golferIds = GolferIds();
            var expected = 30;

            var expectedMatches = GolfMatches();

            var actual = new ScheduleYearlySchedule().Schedule(matches, golferIds);

            Assert.True(expected == actual.Count());

            foreach (var a in actual)
            {
                Assert.Contains(a, expectedMatches, new GolfMatchComparer());
            }
        }
        //1,2,3,4,5,6,7,8,9,10,11,12,13,0
        //1,0,2,3,4,5,6,7,8,9,10,11,12,13
        //1,13,0,2,3,4,5,6,7,8,9,10,11,12
        //1,9,10,11,12,13,0,2,3,4,5,6,7,8
        [Fact]
        public void ScheduleTheScheduleWithBlinds()
        {
            var matches = MatchSchedulesBlindData();
            var golferIds = GolferIdsBlind();
            var expected = 25;
            var expectedMatches = GolfMatchesBlind();

            var actual = new ScheduleYearlySchedule().Schedule(matches, golferIds);

            Assert.True(expected == actual.Count());

            foreach(var a in actual)
            {
                Assert.Contains(a, expectedMatches, new GolfMatchComparer());
            }
        }

        public class GolfMatchComparer : IEqualityComparer<GolfMatch>
        {
            public bool Equals(GolfMatch? x, GolfMatch? y) =>
                x?.GolferId == y?.GolferId && x?.MatchScheduleId == y?.MatchScheduleId;

            public int GetHashCode(GolfMatch obj) =>
                HashCode.Combine(obj.GolferId, obj.MatchScheduleId);
        }

        //[1,2,3,4,5,0]
        //[1,0,2,3,4,5]
        //[1,5,0,2,3,4]
        //[1,4,5,0,2,3]
        //[1,3,4,5,0,2]
        private List<GolfMatch> GolfMatchesBlind()
        {
            var golfMatches = new[]
            {
                new GolfMatch { GolferId = 1, MatchScheduleId = 3 },
                new GolfMatch { GolferId = 2, MatchScheduleId = 1 },
                new GolfMatch { GolferId = 5, MatchScheduleId = 1 },
                new GolfMatch { GolferId = 3, MatchScheduleId = 2 },
                new GolfMatch { GolferId = 4, MatchScheduleId = 2 },

                new GolfMatch { GolferId = 1, MatchScheduleId = 4 },
                new GolfMatch { GolferId = 5, MatchScheduleId = 4 },
                new GolfMatch { GolferId = 4, MatchScheduleId = 6 },
                new GolfMatch { GolferId = 2, MatchScheduleId = 5 },
                new GolfMatch { GolferId = 3, MatchScheduleId = 5 },

                new GolfMatch { GolferId = 1, MatchScheduleId = 7 },
                new GolfMatch { GolferId = 4, MatchScheduleId = 7 },
                new GolfMatch { GolferId = 5, MatchScheduleId = 8 },
                new GolfMatch { GolferId = 3, MatchScheduleId = 8 },
                new GolfMatch { GolferId = 2, MatchScheduleId = 9 },

                new GolfMatch { GolferId = 1, MatchScheduleId = 10 },
                new GolfMatch { GolferId = 3, MatchScheduleId = 10 },
                new GolfMatch { GolferId = 4, MatchScheduleId = 11 },
                new GolfMatch { GolferId = 2, MatchScheduleId = 11 },
                new GolfMatch { GolferId = 5, MatchScheduleId = 12 },

                new GolfMatch { GolferId = 1, MatchScheduleId = 13 },
                new GolfMatch { GolferId = 2, MatchScheduleId = 13 },
                new GolfMatch { GolferId = 3, MatchScheduleId = 15 },
                new GolfMatch { GolferId = 4, MatchScheduleId = 14 },
                new GolfMatch { GolferId = 5, MatchScheduleId = 14 },
            };

            return [.. golfMatches];
        }

        //[1,2,3,4,5,6]
        //[1,6,2,3,4,5]
        //[1,5,6,2,3,4]
        //[1,4,5,6,2,3]
        //[1,3,4,5,6,2]
        private List<GolfMatch> GolfMatches()
        {
            var golfMatches = new[]
            {
                new GolfMatch { GolferId = 1, MatchScheduleId = 1 },
                new GolfMatch { GolferId = 6, MatchScheduleId = 1 },
                new GolfMatch { GolferId = 2, MatchScheduleId = 2 },
                new GolfMatch { GolferId = 5, MatchScheduleId = 2 },
                new GolfMatch { GolferId = 3, MatchScheduleId = 3 },
                new GolfMatch { GolferId = 4, MatchScheduleId = 3 },

                new GolfMatch { GolferId = 1, MatchScheduleId = 5 },
                new GolfMatch { GolferId = 5, MatchScheduleId = 5 },
                new GolfMatch { GolferId = 6, MatchScheduleId = 6 },
                new GolfMatch { GolferId = 4, MatchScheduleId = 6 },
                new GolfMatch { GolferId = 2, MatchScheduleId = 7 },
                new GolfMatch { GolferId = 3, MatchScheduleId = 7 },

                new GolfMatch { GolferId = 1, MatchScheduleId = 9 },
                new GolfMatch { GolferId = 4, MatchScheduleId = 9 },
                new GolfMatch { GolferId = 5, MatchScheduleId = 10 },
                new GolfMatch { GolferId = 3, MatchScheduleId = 10 },
                new GolfMatch { GolferId = 6, MatchScheduleId = 11 },
                new GolfMatch { GolferId = 2, MatchScheduleId = 11 },

                new GolfMatch { GolferId = 1, MatchScheduleId = 13 },
                new GolfMatch { GolferId = 3, MatchScheduleId = 13 },
                new GolfMatch { GolferId = 4, MatchScheduleId = 14 },
                new GolfMatch { GolferId = 2, MatchScheduleId = 14 },
                new GolfMatch { GolferId = 5, MatchScheduleId = 15 },
                new GolfMatch { GolferId = 6, MatchScheduleId = 15 },

                new GolfMatch { GolferId = 1, MatchScheduleId = 17 },
                new GolfMatch { GolferId = 2, MatchScheduleId = 17 },
                new GolfMatch { GolferId = 3, MatchScheduleId = 18 },
                new GolfMatch { GolferId = 6, MatchScheduleId = 18 },
                new GolfMatch { GolferId = 4, MatchScheduleId = 19 },
                new GolfMatch { GolferId = 5, MatchScheduleId = 19 },
            };

            return [.. golfMatches];
        }

        private List<MatchSchedule> MatchSchedulesBlindData()
        {
            var matches = new[]
            {
                new MatchSchedule { MatchScheduleId = 1, Week = 1, Date = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 2, Week = 1, Date = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 3, Week = 1, Date = DateTime.Today, Blind = true},
                new MatchSchedule { MatchScheduleId = 4, Week = 2, Date = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 5, Week = 2, Date = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 6, Week = 2, Date = DateTime.Today, Blind = true},
                new MatchSchedule { MatchScheduleId = 7, Week = 3, Date = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 8, Week = 3, Date = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 9, Week = 3, Date = DateTime.Today, Blind = true},
                new MatchSchedule { MatchScheduleId = 10, Week = 4, Date = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 11, Week = 4, Date = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 12, Week = 4, Date = DateTime.Today, Blind = true},
                new MatchSchedule { MatchScheduleId = 13, Week = 5, Date = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 14, Week = 5, Date = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 15, Week = 5, Date = DateTime.Today, Blind = true},
                new MatchSchedule { MatchScheduleId = 92, MajorId = 1},
                new MatchSchedule { MatchScheduleId = 93, MajorId = 2},
                new MatchSchedule { MatchScheduleId = 94, MajorId = 3},
                new MatchSchedule { MatchScheduleId = 95, MajorId = 14}
            };

            return [.. matches];
        }

        private List<int> GolferIdsBlind() => new() { 1, 2, 3, 4, 5, };

        private List<MatchSchedule> MatchSchedulesData()
        {
            //Left gaps in ids to be sure it still works
            var matches = new[]
            {
                new MatchSchedule { MatchScheduleId = 1, Week = 1, Date = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 2,Week = 1, Date = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 3,Week = 1, Date = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 5,Week = 2, Date = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 6,Week = 2, Date = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 7,Week = 2, Date = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 9,Week = 3, Date = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 10,Week = 3, Date = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 11,Week = 3, Date = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 13,Week = 4, Date = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 14,Week = 4, Date = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 15,Week = 4, Date = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 17,Week = 5, Date = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 18,Week = 5, Date = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 19,Week = 5, Date = DateTime.Today},
                new MatchSchedule { MatchScheduleId = 92, MajorId = 1},
                new MatchSchedule { MatchScheduleId = 93, MajorId = 2},
                new MatchSchedule { MatchScheduleId = 94, MajorId = 3},
                new MatchSchedule { MatchScheduleId = 95, MajorId = 14}
            };

            return [.. matches];
        }
        private List<int> GolferIds() => new() { 1, 2, 3, 4, 5, 6 };

        //
        //Was playing around with HashSets, and it makes it easy to get the pairs, but setting the weeks was a pain.  Leaving this here for knowledge on it.
        //
        //private List<GolfMatch> FakeGolfMatchesThirteen()
        //{
        //    var golfMatches = new List<GolfMatch>();
        //    var pairs = new HashSet<(int, int)>();
        //    var golfers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 0 };
        //    var shorten = 0;
        //    var start = 1;
        //    var end = 13;

        //    for (int i = 0; i < golfers.Count-1; i++)
        //    {
        //        while(start <= end-shorten)
        //        {
        //            var last = golfers.LastOrDefault();

        //            if (golfers[i] == 0)
        //            {
        //                var (a, b) = golfers[i] < last ? (golfers[i], last) : (last, golfers[i]);
        //                pairs.Add((a,b));
        //            }

        //            else if (last == 0)
        //            {
        //                var (a, b) = golfers[i] < last ? (golfers[i], last) : (last, golfers[i]);
        //                pairs.Add((a, b));
        //            }
        //            else
        //            {
        //                var (a, b) = golfers[i] < last ? (golfers[i], last) : (last, golfers[i]);
        //                pairs.Add((a,b));
        //            }

        //            golfers.RemoveAt(golfers.Count - 1);
        //            golfers.Insert(i+1, last);
        //            start++;
        //        }
        //        end += 13;
                
        //        shorten++;
        //    }

        //    int counter = 1;
        //    int blindCounter = 7;
        //    foreach(var pair in pairs)
        //    {
        //        if (pair.Item1 == 0)
        //        {
        //            var match = new GolfMatch { GolferId = pair.Item2, MatchScheduleId = blindCounter };
        //            blindCounter += 7;
        //            golfMatches.Add(match);
        //        }
        //        else
        //        {
        //            var matchA = new GolfMatch { GolferId = pair.Item1, MatchScheduleId = counter };
        //            counter++;
        //            var matchB = new GolfMatch { GolferId = pair.Item2, MatchScheduleId = counter };
        //            counter++;
        //            golfMatches.Add(matchA);
        //            golfMatches.Add(matchB);
        //        }
        //    }
        //    return golfMatches;
        //}
    }
}
