namespace GolfHandicap.Features.Matches.Post.Schedules
{
    public record ScheduleRequest(DateTime startDate, IEnumerable<MajorScheduleRequest> majors, int flightId);
}
