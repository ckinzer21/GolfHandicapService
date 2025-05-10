using GolfHandicap.Common;
using GolfHandicap.Data;
using GolfHandicap.Entities;
using GolfHandicap.Features.Matches.Post.GolfMatches;
using GolfHandicap.Features.Matches.Post.Schedules;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            var golfers = await _context.Golfers.Where(g => g.FlightId == request.flightId).ToListAsync();

            var matchSchedule = _generateMatchSchedule.Generate(request, golfers.ToList());
            _context.MatchSchedules.AddRange(matchSchedule);
            await _context.SaveChangesAsync();

            var golferIds = golfers.Select(g => g.GolferId).ToList();
            var golfmatches = _scheduleYearlySchedule.Schedule(matchSchedule, golferIds);
            _context.GolfMatches.AddRange(golfmatches);
            await _context.SaveChangesAsync();
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