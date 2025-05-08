using GolfHandicap.Common;
using GolfHandicap.Data;
using GolfHandicap.Entities;
using GolfHandicap.Features.Scores.Handicaps.Calculation;
using Microsoft.EntityFrameworkCore;

namespace GolfHandicap.Features.Scores.Post
{
    public class PostScoreHandler : IPostScoreHandler
    {
        private readonly DataContext _context;
        private readonly IHandicapCalculation _handicapCalculation;
        private readonly ICustomRounding _customRounding;

        public PostScoreHandler(DataContext context, IHandicapCalculation handicapCalculation, ICustomRounding customRounding)
        {
            _context = context;
            _handicapCalculation = handicapCalculation;
            _customRounding = customRounding;
        }

        public async Task<(double, int)> CreateScore(PostScoreRequest request)
        {
            var score = new Score { GrossStrokes = request.Strokes, AdjustedGrossStrokes = request.AdjustedStrokes, GolferId = request.GolferId, MatchScheduleId = request.MatchScheduleId };

            _context.Add(score);
            await _context.SaveChangesAsync();

            return await GetHandicap(request);
        }

        public async Task<(double, int)> UpdateScore(PostScoreRequest request)
        {
            var score = await _context.Scores.FirstOrDefaultAsync(s => s.ScoreId == request.ScoreId);

            if (score == null) throw new Exception("Score not found"); //need to fix

            score.GrossStrokes = request.Strokes;
            score.AdjustedGrossStrokes = request.AdjustedStrokes;
            score.GolferId = request.GolferId;
            score.MatchScheduleId = request.MatchScheduleId;
            await _context.SaveChangesAsync();

            return await GetHandicap(request);            
        }

        private async Task<(double, int)> GetHandicap(PostScoreRequest request)
        {
            var lastSixScores = await GetLastSixScores(request);
            var weight = await _context.Weight.FirstOrDefaultAsync();
            if (weight != null)
            {
                var handicapIndex = _handicapCalculation.CalculateHandicapIndex(lastSixScores, weight);
                var roundedHandicapIndex = _customRounding.RoundHalfUpElseFloor(handicapIndex);
                return (handicapIndex, roundedHandicapIndex);
            }
            else throw new Exception("No Weight found");
        }

        private async Task<List<Score>> GetLastSixScores(PostScoreRequest request) => 
            await _context.Scores
                .Where(s => s.GolferId == request.GolferId)
                .Include(s => s.MatchSchedule) // ensures matchschedule is loaded
                .OrderByDescending(s => s.MatchSchedule.Year)
                .ThenByDescending(s => s.MatchSchedule.Week)
                .Take(6)
                .ToListAsync();

    }
}
