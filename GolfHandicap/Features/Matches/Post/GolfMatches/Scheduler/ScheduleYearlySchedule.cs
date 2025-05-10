using GolfHandicap.Entities;

namespace GolfHandicap.Features.Matches.Post.GolfMatches.Scheduler
{
    public class ScheduleYearlySchedule : IScheduleYearlySchedule
    {
        //Round-Robin Rotating table
        public IEnumerable<GolfMatch> Schedule(IEnumerable<MatchSchedule> matchSchedules, List<int> golferIds)
        {
            var result = new List<GolfMatch>();

            var filteredMatches = matchSchedules.Where(m => m.MajorId == null);

            var groupedByWeek = filteredMatches
                .GroupBy(ms => new { ms.Week, ms.Date.Year })
                .OrderBy(g => g.Key.Year)
                .ThenBy(g => g.Key.Week);

            var matchesPerWeek = (int)Math.Floor((decimal)golferIds.Count / 2);
            var fixedGolfer = golferIds.FirstOrDefault();
            if (golferIds.Count % 2 != 0) golferIds.Add(0);//blind holder

            //blind will be at the end of list, so fixed golfer will play them first, then it will rotate
            //[1][2,3,4,5,6,7] | [8/9/10/11/12/13/Blind]
            //(1,Blind)(2,13)(3,12)...
            //Rotate clockwise
            //[1][Blind,2,3,4,5,6,7] | [8,9,10,11,12,13]
            //(1,13)(Blind,12)(2,11) ...

            foreach (var weekGroup in groupedByWeek)
            {
                var weekResult = new List<GolfMatch>();
                var schedules = weekGroup.OrderBy(ms => ms.MatchScheduleId).ToList();
                var matchSlots = schedules.Where(ms => !ms.Blind).ToList();
                var blindSlot = schedules.FirstOrDefault(ms => ms.Blind);
                var matchIndex = 0;
                var wasBlindSet = false;

                for (int i = 1; matchIndex < matchesPerWeek; i++)
                {
                    for (int j = golferIds.Count - i; matchIndex < matchesPerWeek; j--)
                    {
                        // fixedGolfer always plays last index.  should happen first before rotation
                        if (j == golferIds.Count - 1)
                        {
                            if (golferIds[j] == 0 && blindSlot is not null)
                            {
                                weekResult.Add(new GolfMatch { MatchScheduleId = blindSlot.MatchScheduleId, GolferId = fixedGolfer });
                                wasBlindSet = true;
                                continue;
                            }
                            weekResult.Add(new GolfMatch { MatchScheduleId = matchSlots[matchIndex].MatchScheduleId, GolferId = fixedGolfer });
                            weekResult.Add(new GolfMatch { MatchScheduleId = matchSlots[matchIndex].MatchScheduleId, GolferId = golferIds[j] });
                            matchIndex++;
                        }
                        else
                        {
                            if ((golferIds[j] == 0 || golferIds[i] == 0) && blindSlot is not null)
                            {
                                if (golferIds[i] == 0)
                                    weekResult.Add(new GolfMatch { MatchScheduleId = blindSlot.MatchScheduleId, GolferId = golferIds[j] });
                                if (golferIds[j] == 0)
                                    weekResult.Add(new GolfMatch { MatchScheduleId = blindSlot.MatchScheduleId, GolferId = golferIds[i] });
                                i++;
                                wasBlindSet = true;
                                continue;
                            }
                            else
                            {
                                weekResult.Add(new GolfMatch { MatchScheduleId = matchSlots[matchIndex].MatchScheduleId, GolferId = golferIds[i] });
                                weekResult.Add(new GolfMatch { MatchScheduleId = matchSlots[matchIndex].MatchScheduleId, GolferId = golferIds[j] });
                            }
                            matchIndex++;
                            i++;
                        }

                        // Handles if the blind is the last one to get picked up, otherwise matchIndex < matchesPerWeek and won't set it
                        if (!wasBlindSet && matchIndex == matchesPerWeek)
                        {
                            j--;
                            if ((golferIds[j] == 0 || golferIds[i] == 0) && blindSlot is not null)
                            {
                                if (golferIds[i] == 0)
                                    weekResult.Add(new GolfMatch { MatchScheduleId = blindSlot.MatchScheduleId, GolferId = golferIds[j] });
                                if (golferIds[j] == 0)
                                    weekResult.Add(new GolfMatch { MatchScheduleId = blindSlot.MatchScheduleId, GolferId = golferIds[i] });
                            }
                        }
                    }
                }

                //Rotate the last value to the beginning, ahead of the fixed
                var lastIndexValue = golferIds.LastOrDefault();
                golferIds.RemoveAt(golferIds.Count - 1);
                golferIds.Insert(1, lastIndexValue);
                result.AddRange(weekResult);
            }

            return result;
        }
    }
}
