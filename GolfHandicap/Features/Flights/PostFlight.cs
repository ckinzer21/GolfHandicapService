using GolfHandicap.Data;
using GolfHandicap.Entities;

namespace GolfHandicap.Features.Flights
{
    public class PostFlight : IPostFlight
    {
        private readonly DataContext _context;

        public PostFlight(DataContext context)
        {
            _context = context;
        }

        public async Task CreateFlight(IEnumerable<PostFlightRequest> requests)
        {
            _context.Database.EnsureCreated();

            foreach (var request in requests)
            {
                var flight = new Flight { FlightId = request.flightId, Name = request.name };
                _context.Add(flight);
            }
            await _context.SaveChangesAsync();
        }
    }
}
