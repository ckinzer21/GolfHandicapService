using GolfHandicap.Data;
using GolfHandicap.Entities;

namespace GolfHandicap.Features.Matches.Post.Schedule
{
    public class PostMatchScheduleHandler : IPostMatchScheduleHandler
    {
        private readonly DataContext _context;

        public PostMatchScheduleHandler(DataContext context)
        {
            _context = context;
        }

        // after learning more about RESTful API's, see what is better to return other than Task
        // I assume something related to a Result
        public async Task CreateYearlySchedule(IEnumerable<PostMatchScheduleRequest> requests)
        {
            _context.Database.EnsureCreated();

            foreach (var request in requests)
            {
                var match = new MatchSchedule { Year = request.year, Week = request.week };
                _context.Add(match);
                await _context.SaveChangesAsync();
            }
        }
    }
}