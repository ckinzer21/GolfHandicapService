namespace GolfHandicap.Features.Matches.Post.Schedule
{
    public interface IPostMatchScheduleHandler
    {
        public Task CreateYearlySchedule(IEnumerable<PostMatchScheduleRequest> requests);
    }
}
