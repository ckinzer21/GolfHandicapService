using GolfHandicap.Data;
using GolfHandicap.Entities;
using Microsoft.EntityFrameworkCore;

namespace GolfHandicap.Features.Scores.Post
{
    public class PostScoreHandler : IPostScoreHandler
    {
        private readonly DataContext _context;

        public PostScoreHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<PostScoreResposne> CreateScore(PostScoreRequest request)
        {
            var score = new Score { GrossStrokes = request.Strokes, AdjustedGrossStrokes = request.AdjustedStrokes, GolferId = request.GolferId, MatchId = request.MatchId };

            _context.Add(score);
            await _context.SaveChangesAsync();

            return new PostScoreResposne(score.ScoreId, score.GrossStrokes, score.AdjustedGrossStrokes, score.GolferId, score.MatchId);
        }

        public async Task UpdateScore(PostScoreRequest request)
        {
            var score = await _context.Scores.FirstOrDefaultAsync(s => s.ScoreId == request.ScoreId);

            if (score == null) return; //need to fix

            score.GrossStrokes = request.Strokes;
            score.AdjustedGrossStrokes = request.AdjustedStrokes;
            score.GolferId = request.GolferId;
            score.MatchId = request.MatchId;
            await _context.SaveChangesAsync();
        }
    }
}
