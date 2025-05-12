using AutoMapper;
using AutoMapper.QueryableExtensions;
using GolfHandicap.Common;
using GolfHandicap.Common.Handicaps;
using GolfHandicap.Data;
using Microsoft.EntityFrameworkCore;

namespace GolfHandicap.Features.Scores.Get
{
    public class GetScoreHandler : IGetScoreHandler
    {
        private readonly DataContext _context;
        private readonly IGetHandicap _getHandicap;
        private readonly IMapper _mapper;

        public GetScoreHandler(DataContext context, IGetHandicap getHandicap, IMapper mapper)
        {
            _context = context;
            _getHandicap = getHandicap;
            _mapper = mapper;
        }

        public async Task<GetScoreResponse?> GetScoreByScoreId(int id)
        {
            return await _context.Score
                .AsNoTracking()
                .ProjectTo<GetScoreResponse>(_mapper.ConfigurationProvider)
                .Where(x => x.ScoreId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<GetScoreResponse?>> GetScoresByGolferId(int golferId)
        {
            return await _context.Score
                .AsNoTracking()
                .ProjectTo<GetScoreResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<HandicapIndexResult> GetHandicapIndex(int golferId)
        {
            return await _getHandicap.GetIndexAndRounded(golferId);
        }
    }
}
