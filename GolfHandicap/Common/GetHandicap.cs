using GolfHandicap.Data;
using GolfHandicap.Entities;
using GolfHandicap.Features.Scores.Handicaps.Calculation;
using Microsoft.EntityFrameworkCore;

namespace GolfHandicap.Common
{
    public class GetHandicap : IGetHandicap
    {
        private readonly IHandicapCalculation _handicapCalculation;
        private readonly ICustomRounding _customRounding;
        private readonly DataContext _context;

        public GetHandicap(IHandicapCalculation handicapCalculation, ICustomRounding customRounding, DataContext context)
        {
            _handicapCalculation = handicapCalculation;
            _customRounding = customRounding;
            _context = context;
        }

        public async Task<HandicapIndexResult> GetIndexAndRounded(int golferId)
        {
            var lastSixScores = await GetLastSixScores(golferId);
            if (lastSixScores.Count == 0) return new HandicapIndexResult { Error = "No scores available." };
            var weight = await _context.Weight.FirstOrDefaultAsync();
            if (weight != null)
            {
                var handicapIndex = _handicapCalculation.CalculateHandicapIndex(lastSixScores, weight);
                var roundedHandicapIndex = _customRounding.RoundHalfUpElseFloor(handicapIndex);
                return new HandicapIndexResult { HandicapIndex = handicapIndex, RoundedHandicap = roundedHandicapIndex };
            }
            else return new HandicapIndexResult { Error = "No weight record found."};
        }

        private async Task<List<Score>> GetLastSixScores(int golferId) =>
            await _context.Scores
                .Where(s => s.GolferId == golferId && s.MatchSchedule != null)
                .Include(s => s.MatchSchedule) // ensures matchschedule is loaded
                .Include(s => s.Tee)
                .OrderByDescending(s => s.MatchSchedule.Date.Year)
                .ThenByDescending(s => s.MatchSchedule.Week)
                .Take(6)
                .ToListAsync();
    }
}
