using GolfHandicap.Common;
using GolfHandicap.Data;
using GolfHandicap.Entities;
using Microsoft.EntityFrameworkCore;

namespace GolfHandicap.Features.Scores.Post
{
    public class PostScoreHandler : IPostScoreHandler
    {
        private readonly DataContext _context;
        private readonly IGetHandicap _getHandicap;

        public PostScoreHandler(DataContext context, IGetHandicap getHandicap)
        {
            _context = context;
            _getHandicap = getHandicap;
        }

        public async Task<HandicapIndexResult> CreateScore(PostScoreRequest request)
        {
            var score = new Score { GrossStrokes = request.Strokes, AdjustedGrossStrokes = request.AdjustedStrokes, GolferId = request.GolferId, MatchScheduleId = request.MatchScheduleId, TeeId = request.TeeId };

            _context.Add(score);
            await _context.SaveChangesAsync();

            return await _getHandicap.GetIndexAndRounded(request.GolferId);
        }

        public async Task<HandicapIndexResult> UpdateScore(PostScoreRequest request)
        {
            var score = await _context.Scores.FirstOrDefaultAsync(s => s.ScoreId == request.ScoreId);

            if (score == null) throw new Exception("Score not found"); //need to fix

            score.GrossStrokes = request.Strokes;
            score.AdjustedGrossStrokes = request.AdjustedStrokes;
            score.GolferId = request.GolferId;
            score.MatchScheduleId = request.MatchScheduleId;
            await _context.SaveChangesAsync();

            return await _getHandicap.GetIndexAndRounded(request.GolferId);
        }
    }
}
