using GolfHandicap.Common;
using GolfHandicap.Data;
using Microsoft.EntityFrameworkCore;

namespace GolfHandicap.Features.Scores.Get
{
    public class GetScoreHandler : IGetScoreHandler
    {
        private readonly DataContext _context;
        private readonly IGetHandicap _getHandicap;

        public GetScoreHandler(DataContext context, IGetHandicap getHandicap)
        {
            _context = context;
            _getHandicap = getHandicap;
        }

        public async Task<GetScoreResponse?> GetScoreByScoreId(int id)
        {
            return await _context.Score
                .Where(x => x.ScoreId == id)
                .Select(x => new GetScoreResponse(x.ScoreId, x.GrossStrokes, x.AdjustedGrossStrokes, x.MatchScheduleId, x.GolferId, x.TeeId))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<GetScoreResponse?>> GetScoresByGolferId(int golferId)
        {
            return await _context.Score.Select(x => new GetScoreResponse(x.ScoreId, x.GrossStrokes, x.AdjustedGrossStrokes, x.MatchScheduleId, x.GolferId, x.TeeId)).ToListAsync();
        }

        public async Task<HandicapIndexResult> GetHandicapIndex(int golferId)
        {
            return await _getHandicap.GetIndexAndRounded(golferId);
        }
    }
}
