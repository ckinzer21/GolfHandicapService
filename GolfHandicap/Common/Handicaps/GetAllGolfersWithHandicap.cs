using AutoMapper;
using AutoMapper.QueryableExtensions;
using GolfHandicap.Data;
using GolfHandicap.Features.Golfers.Get;
using Microsoft.EntityFrameworkCore;

namespace GolfHandicap.Common.Handicaps
{
    public interface IGetAllGolfersWithHandicap
    {
        Task<IEnumerable<GetGolferResponse>> Get();
    }

    public class GetAllGolfersWithHandicap : IGetAllGolfersWithHandicap
    {
        private readonly DataContext _context;
        private readonly IGetHandicap _getHandicap;
        private readonly IMapper _mapper;

        public GetAllGolfersWithHandicap(DataContext context, IGetHandicap getHandicap, IMapper mapper)
        {

            _context = context;
            _getHandicap = getHandicap;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetGolferResponse>> Get()
        {
            var golfers = await _context.Golfer
                .AsNoTracking()
                .ProjectTo<GetGolferResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (golfers.Count == 0) return new List<GetGolferResponse>();

            var tasks = golfers.Select(async golfer =>
            {
                var handicap = await _getHandicap.GetIndexAndRounded(golfer.GolferId, true);
                golfer.HandicapIndex = handicap.HandicapIndex;
                golfer.RoundedHandicap = handicap.RoundedHandicap;
            });

            await Task.WhenAll(tasks);

            return golfers;
        }
    }
}
