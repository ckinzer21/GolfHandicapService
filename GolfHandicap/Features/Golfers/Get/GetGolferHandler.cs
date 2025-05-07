using AutoMapper;
using GolfHandicap.Data;
using Microsoft.EntityFrameworkCore;

namespace GolfHandicap.Features.Golfers.Get.GetById
{
    public class GetGolferHandler : IGetGolferHandler
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GetGolferHandler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetGolferResponse?> GetGolferById(int id)
        {
            return await _context.Golfers
                .Where(g => g.GolferId == id)
                .Select(g => new GetGolferResponse(g.GolferId, g.Name, g.Email, g.IsDeleted))
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<GetGolferResponse?>> GetAllGolfers()
        {
            return await _context.Golfers.Select(g => new GetGolferResponse(g.GolferId, g.Name, g.Email, g.IsDeleted)).ToListAsync();
        }
    }
}
