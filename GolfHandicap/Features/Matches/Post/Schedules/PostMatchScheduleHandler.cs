using GolfHandicap.Common;
using GolfHandicap.Data;
using GolfHandicap.Features.Matches.Post.GolfMatches;
using GolfHandicap.Features.Matches.Post.Schedules;
using Microsoft.EntityFrameworkCore;

namespace GolfHandicap.Features.Matches.Post.Schedule
{
    public class PostMatchScheduleHandler : IPostMatchScheduleHandler
    {
        private readonly DataContext _context;
        private readonly IGenerateMatchSchedule _generateMatchSchedule;
        private readonly IScheduleYearlySchedule _scheduleYearlySchedule;

        public PostMatchScheduleHandler(DataContext context, IGenerateMatchSchedule generateMatchSchedule, IScheduleYearlySchedule scheduleYearlySchedule)
        {
            _context = context;
            _generateMatchSchedule = generateMatchSchedule;
            _scheduleYearlySchedule = scheduleYearlySchedule;
        }
                
        public async Task CreateScheduleByFlight(ScheduleRequest request)
        {
            var golfers = await _context.Golfer.Where(g => g.FlightId == request.flightId).ToListAsync();

            var matchSchedule = _generateMatchSchedule.Generate(request, golfers.ToList());
            _context.MatchSchedule.AddRange(matchSchedule);
            await _context.SaveChangesAsync();

            var golferIds = golfers.Select(g => g.GolferId).ToList();
            var golfmatches = _scheduleYearlySchedule.Schedule(matchSchedule, golferIds);
            var random = Random.Shared;
            var shuffledMatches = golfmatches.OrderBy(_ => random.Next()).ToList();
            _context.GolfMatch.AddRange(shuffledMatches);
            await _context.SaveChangesAsync();
        }
    }
}