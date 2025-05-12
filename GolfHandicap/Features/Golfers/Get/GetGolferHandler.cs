using AutoMapper;
using AutoMapper.QueryableExtensions;
using GolfHandicap.Common;
using GolfHandicap.Data;
using Microsoft.EntityFrameworkCore;

namespace GolfHandicap.Features.Golfers.Get.GetById
{
    public class GetGolferHandler : IGetGolferHandler
    {
        private readonly DataContext _context;
        private readonly IGetHandicap _getHandicap;
        private readonly IMapper _mapper;

        public GetGolferHandler(DataContext context, IGetHandicap getHandicap, IMapper mapper)
        {
            _context = context;
            _getHandicap = getHandicap;
            _mapper = mapper;
        }

        public async Task<GetGolferResponse?> GetGolferById(int golferId)
        {
            var golfer = await _context.Golfer
                .AsNoTracking()
                .ProjectTo<GetGolferResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(g => g.GolferId == golferId);

            if (golfer == null) throw new Exception("No Golfer Found");

            HandicapIndexResult handicap = await _getHandicap.GetIndexAndRounded(golfer.GolferId);
            golfer.HandicapIndex = handicap.HandicapIndex;
            golfer.RoundedHandicap = handicap.RoundedHandicap;
            
            return new GetGolferResponse();
        }

        public async Task<IEnumerable<GetGolferResponse?>> GetAllGolfers()
        {
            var golfers = await _context.Golfer
                .AsNoTracking()
                .ProjectTo<GetGolferResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (golfers.Count == 0) throw new Exception("No Golfers Found");

            var tasks = golfers.Select(async golfer =>
            {
                var handicap = await _getHandicap.GetIndexAndRounded(golfer.GolferId);
                golfer.HandicapIndex = handicap.HandicapIndex;
                golfer.RoundedHandicap = handicap.RoundedHandicap;
            });

            await Task.WhenAll(tasks);//When all for async.  Wait all when called from a synchronous method

            return golfers;
        }
    }
}
