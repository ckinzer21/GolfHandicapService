using GolfHandicap.Entities;
using GolfHandicap.Features.Matches.Post.Schedules;

namespace GolfHandicap.Common
{
    public interface IGenerateMatchSchedule
    {
        IEnumerable<MatchSchedule> Generate(ScheduleRequest request, List<Golfer> golfers);
    }
}
