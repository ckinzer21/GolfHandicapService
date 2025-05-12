using GolfHandicap.Common;
using GolfHandicap.Entities;
using GolfHandicap.Features.Matches.Post.Schedules;

namespace GolfHandicap.Features.Matches.Post.GolfMatches.Scheduler
{
    //my thoughts, I should pass in the starting date and the major dates
    //then I'll grab the golfers from A flight, create a list of the majors schedule
    // create a list of the match schedules, then find out the weeks.  so week 1 is a major, week 2-6 reg, week 7 major
    // Then I can save this, then I can schedule the actual matches
    // Total Mathces = (n(n-1)/2)+Majors where n = total number of golfers
    // Matches per week = n/2
    // Total weeks, if even = n-1, if odd = n
    //this will allow me to add dates for them
    //then grab B flight and do the same
    public class GenerateMatchSchedule : IGenerateMatchSchedule
    {
        //this method let me know that AI can be crap
        //this current year for my league in A flight, there are 13 regular weeks, and 4 major weeks
        //there are 13 players in A flight, so per week there are 7 matches(one person plays the blind)
        // 7*13=91 + the 4 majors = 95
        //when i tried to get a formula from AI, it kept saying I should only have 78 matches.
        //then I put this code into it, and it tried to optimize it, giving me 94 matches, but it went into week 16???
        public IEnumerable<MatchSchedule> Generate(ScheduleRequest request, List<Golfer> golfers)
        {
            var majorSchedule = new List<MatchSchedule>();
            foreach (var major in request.majors)
            {
                majorSchedule.Add(new MatchSchedule { MatchDate = major.Date, MajorId = major.MajorId });
            }

            var regularSchedule = new List<MatchSchedule>();

            var participants = golfers.Count;
            var matchesPerWeek = (int)Math.Ceiling((decimal)participants / 2);
            var totalWeeks = participants % 2 == 0 ? participants - 1 : participants;
            int numberOfWeeksSkipped = 0;
            bool isSkippingAWeek = false;

            for (var i = 0; i < totalWeeks; i++)
            {
                for (var j = 0; j < matchesPerWeek; j++)
                {
                    var match = new MatchSchedule();
                    if (majorSchedule.Any(m => m.MatchDate == request.startDate.AddDays((i + numberOfWeeksSkipped) * 7)))
                    {
                        match.MatchDate = request.startDate.AddDays((i + 1) * 7);
                        match.Week = i + 1;
                        if (!isSkippingAWeek)
                        {
                            numberOfWeeksSkipped += 1;
                            isSkippingAWeek = true;
                        }
                    }
                    else
                    {
                        match.MatchDate = request.startDate.AddDays((i + numberOfWeeksSkipped) * 7);
                        match.Week = i + 1;
                    }

                    if (participants % 2 != 0 && j + 1 == matchesPerWeek)
                    {
                        match.Blind = true;
                    }
                    regularSchedule.Add(match);
                }
                isSkippingAWeek = false;
            }

            regularSchedule.AddRange(majorSchedule);
            return regularSchedule;

        }
    }
}
