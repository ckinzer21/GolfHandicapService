using GolfHandicap.Entities;
using GolfHandicap.Features.Golfers.Get;

namespace GolfHandicap.Features.StartOfYear
{
    public interface ISplitFlights
    {
        Task<IEnumerable<GolferStartOfYearResponse>> Run(List<GetGolferResponse> golfers, List<Flight> flights);
    }
    // what if we get a lot more players and end up having 3 flights?
    // maybe take in a parameter for count of flights
    // so if 2 flights, divive by 2, take floor for A, and take the rest for b
    // I believe we are capped at 14 per flight, lets say we have 41 people
    // we would do 13 in A, 14 in B, 14 in C
    // but what if we have like 35 people, we don't want to fill up to max amount, try to split evenly
    // 35/3 = 11.6, so take floor = 11 in A.  Then (35-11)/2 = 12 for both
    // If we have 2 flights, just do both flights at once, if we have more, we'll have to loop
    // so it would do one flight per loop, remove the players who were assigned a flight, then loop again until we finish
    public class SplitFlights : ISplitFlights
    {
        //flights should be coming in ordered by rank, not ordering here
        public async Task<IEnumerable<GolferStartOfYearResponse>> Run(List<GetGolferResponse> golfers, List<Flight> flights)
        {
            if (flights.Count > 1)
            {
                var allAssignedGolfers = new List<GolferStartOfYearResponse>();
                var totalFlights = flights.Count;
                var sortedGolfers = golfers.OrderBy(g => g.HandicapIndex).ToList();
                var golfersPerFlight = (int)Math.Floor((decimal)golfers.Count / totalFlights);

                for (int i = 0; i < totalFlights; i++)
                {
                    List<GetGolferResponse> golfersForFlight;

                    if (i == totalFlights - 1) // last flight gets the rest
                    {
                        golfersForFlight = sortedGolfers;
                    }
                    else
                    {
                        golfersForFlight = sortedGolfers.Take(golfersPerFlight).ToList();
                        sortedGolfers = sortedGolfers.Skip(golfersPerFlight).ToList();
                    }

                    var assignedGolfers = await Task.Run(() => AssignFlights(golfersForFlight, flights[i]));
                    allAssignedGolfers.AddRange(assignedGolfers);
                }

                return allAssignedGolfers;
            }

            return await Task.Run(() => AssignFlights(golfers, flights.FirstOrDefault()!));
        }

        private IEnumerable<GolferStartOfYearResponse> AssignFlights(IEnumerable<GetGolferResponse> golfers, Flight flight)
        {
            var results = new List<GolferStartOfYearResponse>();

            foreach (var golfer in golfers)
            {
                var result = new GolferStartOfYearResponse
                {
                    GolferId = golfer.GolferId,
                    FlightId = flight.FlightId!,
                    Flight = flight.Name,
                    Name = golfer.Name,
                    HandicapIndex = golfer.HandicapIndex
                };
                results.Add(result);
            }
            return results;
        }
    }
}
