using GolfHandicap.Data;
using GolfHandicap.Entities;
using GolfHandicap.Features.Matches.Post.Schedules;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GolfHandicap.Features.Matches.Post.Schedule
{
    public class PostMatchScheduleHandler : IPostMatchScheduleHandler
    {
        private readonly DataContext _context;

        public PostMatchScheduleHandler(DataContext context)
        {
            _context = context;
        }

        
        public async Task CreateScheduleByFlight(ScheduleRequest request)
        {
            var golfers = await _context.Golfers.Select(g => g.FlightId == request.flightId).ToListAsync();

            
        }

        // after learning more about RESTful API's, see what is better to return other than Task
        // I assume something related to a Result
        public async Task CreateYearlySchedule(IEnumerable<PostMatchScheduleRequest> requests)
        {
            _context.Database.EnsureCreated();

            foreach (var request in requests)
            {
                var match = new MatchSchedule { Date = request.date, Week = request.week };
                _context.Add(match);
                await _context.SaveChangesAsync();
            }
        }
    }
}