using AutoMapper;
using GolfHandicap.Common.Handicaps;
using GolfHandicap.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GolfHandicap.Features.StartOfYear
{
    public interface IStartOfYearHandler
    {
        Task<IEnumerable<GolferStartOfYearResponse>> SetupStartOfYearPreview();
        Task<IEnumerable<GolferStartOfYearResponse>> SetupStartOfYear();
    }

    public class StartOfYearHandler : IStartOfYearHandler
    {
        private readonly DataContext _context;
        private readonly IGetAllGolfersWithHandicap _getAllGolfers;
        private readonly ISplitFlights _splitsFlights;

        public StartOfYearHandler(DataContext context, IGetAllGolfersWithHandicap getAllGolfers, ISplitFlights splitsFlights)
        {
            _context = context;
            _getAllGolfers = getAllGolfers;
            _splitsFlights = splitsFlights;

        }

        public async Task<IEnumerable<GolferStartOfYearResponse>> SetupStartOfYear()
        {
            throw new NotImplementedException();
        }


        //At the start of the year, i need to get all of the golfers we have for the league
        //I'll call the handicap calc for all of the golfers, and I'll split down the middle.  Lowest of the middle goes to A, highest goes to B
        //Call the schedule separately.  I'm going to add some preventative logic, like if schedules in the current year exist, return not valid
        //
        public async Task<IEnumerable<GolferStartOfYearResponse>> SetupStartOfYearPreview()
        {
            var golfers = await _getAllGolfers.Get();

            if (golfers.Count() == 0) return new List<GolferStartOfYearResponse>();

            var flights = await _context.Flight.AsNoTracking().ToListAsync();

            return await _splitsFlights.Run(golfers.ToList(), flights);
        }
    }

    public record GolferStartOfYearResponse
    {
        public int GolferId { get; set; }
        public string? Name { get; set; }
        public double? HandicapIndex { get; set; }
        public string? Flight { get; set; }
        public int FlightId { get; set; }
    }
}
