using AutoMapper;
using AutoMapper.QueryableExtensions;
using GolfHandicap.Common;
using GolfHandicap.Common.Handicaps;
using GolfHandicap.Data;
using Microsoft.EntityFrameworkCore;

namespace GolfHandicap.Features.Golfers.Get.GetById
{
    public class GetGolferHandler : IGetGolferHandler
    {
        private readonly DataContext _context;
        private readonly IGetHandicap _getHandicap;
        private readonly IGetAllGolfersWithHandicap _getAllGolfers;
        private readonly IMapper _mapper;

        public GetGolferHandler(DataContext context, IGetHandicap getHandicap, IGetAllGolfersWithHandicap getAllGolfers, IMapper mapper)
        {
            _context = context;
            _getHandicap = getHandicap;
            _mapper = mapper;
            _getAllGolfers = getAllGolfers;
        }

        public async Task<GetGolferResponse?> GetGolferById(int golferId)
        {
            var golfer = await _context.Golfer
                .AsNoTracking()
                .ProjectTo<GetGolferResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(g => g.GolferId == golferId);

            if (golfer == null) return null;

            HandicapIndexResult handicap = await _getHandicap.GetIndexAndRounded(golfer.GolferId);
            golfer.HandicapIndex = handicap.HandicapIndex;
            golfer.RoundedHandicap = handicap.RoundedHandicap;
            
            return new GetGolferResponse();
        }

        public async Task<IEnumerable<GetGolferResponse?>> GetAllGolfers()
        {
            var golfers = await _getAllGolfers.Get();

            return golfers;
        }
    }
}
