using GolfHandicap.Entities;

namespace GolfHandicap.Features.Matches.Post.GolfMatches
{
    public interface IScheduleYearlySchedule
    {
        IEnumerable<GolfMatchDto> Schedule(IEnumerable<MatchSchedule> matchSchedules, List<int> golferIds);
    }
}
