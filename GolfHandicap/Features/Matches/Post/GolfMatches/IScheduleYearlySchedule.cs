using GolfHandicap.Entities;

namespace GolfHandicap.Features.Matches.Post.GolfMatches
{
    public interface IScheduleYearlySchedule
    {
        IEnumerable<GolfMatch> Schedule(IEnumerable<MatchSchedule> matchSchedules, List<int> golferIds);
    }
}
