using GolfHandicap.Entities;
using GolfHandicap.Features.Matches.Post.GolfMatches;

namespace GolfHandicap.Features.Matches.Post.Schedules
{
    public interface IScheduleYearlySchedule
    {
        IEnumerable<GolfMatchDto> Schedule(IEnumerable<MatchSchedule> matchSchedules, List<int> golferIds);
    }

    public class ScheduleYearlySchedule : IScheduleYearlySchedule
    {
        public IEnumerable<GolfMatchDto> Schedule(IEnumerable<MatchSchedule> matchSchedules, List<int> golferIds)
        {
            var random = Random.Shared;
            var result = new List<GolfMatchDto>();

            var groupedByWeek = matchSchedules
                .GroupBy(ms => new { ms.Week, ms.Year })
                .OrderBy(g => g.Key.Year)
                .ThenBy(g => g.Key.Week);

            foreach (var weekGroup in groupedByWeek)
            {
                // What I'm doing is grouping schedule by year and weeks since we our matches are always in the same year during the summer
                // Then we grab those scheduled dates, then schedule with no blinds, and the blind
                // Then we iterate for all the matches that week, adding two golfers at a time
                // After that, we check if there is a blind, then add it if there is one
                var schedules = weekGroup.OrderBy(ms => ms.MatchScheduleId).ToList();
                var matchSlots = schedules.Where(ms => !ms.Blind).ToList();
                var blindSlot = schedules.FirstOrDefault(ms => ms.Blind);

                var shuffledGolfers = golferIds.OrderBy(_ => random.Next()).ToList();
                int i = 0, matchIndex = 0;

                while (i + 1 < shuffledGolfers.Count && matchIndex < matchSlots.Count)
                {
                    var a = shuffledGolfers[i];
                    var b = shuffledGolfers[i + 1];

                    result.Add(new GolfMatchDto { MatchScheduleId = matchSlots[matchIndex].MatchScheduleId, GolferId = a });
                    result.Add(new GolfMatchDto { MatchScheduleId = matchSlots[matchIndex].MatchScheduleId, GolferId = b });

                    i += 2;
                    matchIndex++;
                }

                // Handle blind
                if (i < shuffledGolfers.Count && blindSlot is not null)
                {
                    result.Add(new GolfMatchDto { MatchScheduleId = blindSlot.MatchScheduleId, GolferId = shuffledGolfers[i], Blind = true });
                }
            }

            return result;
        }
    }
}
