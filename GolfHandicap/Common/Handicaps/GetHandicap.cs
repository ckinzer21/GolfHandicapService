using GolfHandicap.Data;
using GolfHandicap.Entities;
using Microsoft.EntityFrameworkCore;

namespace GolfHandicap.Common.Handicaps
{
    public class GetHandicap : IGetHandicap
    {
        private readonly IHandicapCalculation _handicapCalculation;
        private readonly ICustomRounding _customRounding;
        private readonly DataContext _context;
        private const int REGULAR_SEASON_LOOKBACK = 6;
        private const int START_OF_YEAR_LOOKUP = 20;

        public GetHandicap(IHandicapCalculation handicapCalculation, ICustomRounding customRounding, DataContext context)
        {
            _handicapCalculation = handicapCalculation;
            _customRounding = customRounding;
            _context = context;
        }

        public async Task<HandicapIndexResult> GetIndexAndRounded(int? golferId, bool isStartOfYear = false)
        {
            var scoreLookBack = isStartOfYear ? REGULAR_SEASON_LOOKBACK : START_OF_YEAR_LOOKUP;
            var lastSixScores = await GetLastSixScores(golferId, scoreLookBack);
            if (lastSixScores.Count == 0) return new HandicapIndexResult { Error = "No scores available." };
            var weight = await _context.Weight.FirstOrDefaultAsync();
            if (weight != null)
            {
                var handicapIndex = _handicapCalculation.CalculateHandicapIndex(lastSixScores, weight);
                var roundedHandicapIndex = _customRounding.RoundHalfUpElseFloor(handicapIndex);
                return new HandicapIndexResult { HandicapIndex = handicapIndex, RoundedHandicap = roundedHandicapIndex };
            }
            else return new HandicapIndexResult { Error = "No weight record found." };
        }

        private async Task<List<Score>> GetLastSixScores(int? golferId, int scoreLookBack) =>
            await _context.Score
                .AsNoTracking()
                .Where(s => s.GolferId == golferId && s.MatchSchedule != null)
                .Include(s => s.MatchSchedule) // ensures matchschedule is loaded
                .Include(s => s.Tee)
                .OrderByDescending(s => s.MatchSchedule.MatchDate.Year)
                .ThenByDescending(s => s.MatchSchedule.Week)
                .Take(scoreLookBack)
                .ToListAsync();
    }
}
