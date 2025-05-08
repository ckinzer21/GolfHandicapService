using GolfHandicap.Features.Matches.Post.Schedules;

namespace GolfHandicap.Features.Matches.Post.Schedule
{
    public interface IPostMatchScheduleHandler
    {
        public Task CreateYearlySchedule(IEnumerable<PostMatchScheduleRequest> requests);
        public Task CreateScheduleByFlight(ScheduleRequest request);
    }
}
