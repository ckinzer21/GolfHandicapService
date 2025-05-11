using GolfHandicap.Features.Matches.Post.Schedules;

namespace GolfHandicap.Features.Matches.Post.Schedule
{
    public interface IPostMatchScheduleHandler
    {
        public Task CreateScheduleByFlight(ScheduleRequest request);
    }
}
