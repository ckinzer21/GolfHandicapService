using GolfHandicap.Data;
using Microsoft.EntityFrameworkCore;

namespace GolfHandicap.Features.Scores.Get
{
    public class GetScoreHandler : IGetScoreHandler
    {
        private readonly DataContext _context;

        public GetScoreHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<GetScoreResponse?> GetScoreByScoreId(int id)
        {
            return await _context.Scores
                .Where(x => x.ScoreId == id)
                .Select(x => new GetScoreResponse(x.ScoreId, x.GrossStrokes, x.AdjustedGrossStrokes, x.MatchId, x.GolferId, x.CourseId))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<GetScoreResponse?>> GetScoresByGolferId(int golferId)
        {
            return await _context.Scores.Select(x => new GetScoreResponse(x.ScoreId, x.GrossStrokes, x.AdjustedGrossStrokes, x.MatchId, x.GolferId, x.CourseId)).ToListAsync();
        }
    }
}
