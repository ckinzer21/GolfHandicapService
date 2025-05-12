using GolfHandicap.Entities;
using GolfHandicap.Features.Golfers.Get;
using GolfHandicap.Features.Setup.StartOfYear;

namespace GolfHandicap.Test.xUnit
{
    public class SplitFlightsTests
    {
        [Fact]
        public async void OneFlight()
        {
            var flights = OneFlightData();
            var golfers = FlightGolfers(13);
            var expectedCount = 13;
            var expected = Expected(13);

            var actual = await new SplitFlights().Run(golfers, flights);
            Assert.True(expectedCount == actual.Count());

            foreach (var a in actual)
            {
                Assert.Contains(a, expected, new GolferStartOfYearComparer());
            }
        }

        [Fact]
        public async void CoupleFlights()
        {
            var flights = CoupleFlightData();
            var golfers = FlightGolfers(27);
            var expectedAFlight = 13;
            var expectedBFlight = 14;
            var expected = CoupleExpected();

            var actual = await new SplitFlights().Run(golfers, flights);
            Assert.True(expectedAFlight == actual.Count(a => a.Flight == "A"));
            Assert.True(expectedBFlight == actual.Count(b => b.Flight == "B"));
            foreach(var a in actual)
            {
                Assert.Contains(a, expected, new GolferStartOfYearComparer());
            }
        }

        [Fact]
        public async void FewFlights()
        {
            var flights = FewFlightData();
            var golfers = FlightGolfers(39);
            var expectedAFlight = 13;
            var expectedBFlight = 13;
            var expectedCFlight = 13;
            var expected = Expected(39);

            var actual = await new SplitFlights().Run(golfers, flights);
            Assert.True(expectedAFlight == actual.Count(a => a.Flight == "A"));
            Assert.True(expectedBFlight == actual.Count(b => b.Flight == "B"));
            Assert.True(expectedCFlight == actual.Count(b => b.Flight == "C"));

            foreach (var a in actual)
            {
                Assert.Contains(a, expected, new GolferStartOfYearComparer());
            }
        }

        private class GolferStartOfYearComparer : IEqualityComparer<GolferStartOfYearResponse>
        {
            public bool Equals(GolferStartOfYearResponse? x, GolferStartOfYearResponse? y) =>
                x?.GolferId == y?.GolferId && x?.FlightId == y?.FlightId;

            public int GetHashCode(GolferStartOfYearResponse obj) =>
                HashCode.Combine(obj.GolferId, obj.FlightId);
        }

        private List<GolferStartOfYearResponse> FewExpected()
        {
            return Enumerable.Range(1, 39)
                .Select(id => new GolferStartOfYearResponse
                {
                    GolferId = id,
                    FlightId = id <= 13 ? 1 : 2
                }).ToList();
        }

        private List<GolferStartOfYearResponse> Expected(int range)
        {
            return Enumerable.Range(1, range)
                .Select(id => new GolferStartOfYearResponse
                {
                    GolferId = id,
                    FlightId = id <= 13 ? 1 : id <= 26 ? 2 : 3
                }).ToList();
        }

        private List<GolferStartOfYearResponse> CoupleExpected()
        {
            return Enumerable.Range(1, 27)
                .Select(id => new GolferStartOfYearResponse
                {
                    GolferId = id,
                    FlightId = id <= 13 ? 1 : 2
                }).ToList();
        }

        private List<GetGolferResponse> FlightGolfers(int range)
        {
            var golfers = new List<GetGolferResponse>();

            //enumerable range is going into the id
            //i iterates from 0
            golfers.AddRange(Enumerable.Range(1, range)
                .Select((id, i) => new GetGolferResponse
                {
                    GolferId = id,
                    Name = "A",
                    HandicapIndex = 3.4 + i * 0.1
                }));

            return golfers;
        }

        private List<GetGolferResponse> FewFlightGolfers()
        {
            var golfers = new List<GetGolferResponse>();

            // Group A (HandicapIndex: 3.4 to 5.0, GolferId: 1–13)
            golfers.AddRange(Enumerable.Range(1, 13)
                .Select((id, i) => new GetGolferResponse
                {
                    GolferId = id,
                    Name = "A",
                    HandicapIndex = 3.4 + i * 0.1
                }));

            // Group B (HandicapIndex: 13.4 to 15.0, GolferId: 14–26)
            golfers.AddRange(Enumerable.Range(14, 13)
                .Select((id, i) => new GetGolferResponse
                {
                    GolferId = id,
                    Name = "A",
                    HandicapIndex = 13.4 + i * 0.1
                }));

            golfers.AddRange(Enumerable.Range(28, 13)
                .Select((id, i) => new GetGolferResponse
                {
                    GolferId = id,
                    Name = "A",
                    HandicapIndex = 23.4 + i * 0.1
                }));

            return golfers;
        }

        private List<GetGolferResponse> OneFlightGolfers()
        {
            var golfers = new List<GetGolferResponse>();

            golfers.AddRange(Enumerable.Range(1, 13)
                .Select((id, i) => new GetGolferResponse
                {
                    GolferId = id,
                    Name = "A",
                    HandicapIndex = 3.4 + 1 * .01
                }).ToList());

            return golfers;
        }

        private List<Flight> OneFlightData()
        {
            return new List<Flight>
            { 
                new Flight {FlightId = 1, Name = "A", Rank = 1}
            };
        }

        private List<Flight> CoupleFlightData()
        {
            return new List<Flight>
            {
                new Flight {FlightId = 1, Name = "A", Rank = 1},
                new Flight {FlightId = 2, Name = "B", Rank = 2}
            };
        }

        private List<Flight> FewFlightData()
        {
            return new List<Flight>
            {
                new Flight {FlightId = 1, Name = "A", Rank = 1},
                new Flight {FlightId = 2, Name = "B", Rank = 2},
                new Flight {FlightId = 3, Name = "C", Rank = 3}
            };
        }
    }
}
