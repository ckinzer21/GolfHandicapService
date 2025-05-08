using GolfHandicap.Common;
using GolfHandicap.Data;
using Microsoft.EntityFrameworkCore;

namespace GolfHandicap.Features.Golfers.Get.GetById
{
    public class GetGolferHandler : IGetGolferHandler
    {
        private readonly DataContext _context;
        private readonly IGetHandicap _getHandicap;

        public GetGolferHandler(DataContext context, IGetHandicap getHandicap)
        {
            _context = context;
            _getHandicap = getHandicap;
        }

        public async Task<GetGolferResponse?> GetGolferById(int golferId)
        {
            var golfer = await _context.Golfers.FirstOrDefaultAsync(g => g.GolferId == golferId);
            if (golfer == null) throw new Exception("No Golfer Found");
            (double?, int?) handicapIndexAndRounded = await _getHandicap.GetIndexAndRounded(golfer.GolferId);
            return new GetGolferResponse(golfer.GolferId, golfer.Name, golfer.Email, handicapIndexAndRounded.Item1, handicapIndexAndRounded.Item2);
        }

        public async Task<IEnumerable<GetGolferResponse?>> GetAllGolfers()
        {
            var golfers = await _context.Golfers.ToListAsync();
            if (golfers.Count == 0) throw new Exception("No Golfers Found");

            List<GetGolferResponse> getGolferResponses = [];

            foreach (var golfer in golfers)
            {
                (double?, int?) handicapIndexAndRounded = await _getHandicap.GetIndexAndRounded(golfer.GolferId);
                var getGolferResponse = new GetGolferResponse(golfer.GolferId, golfer.Name, golfer.Email, handicapIndexAndRounded.Item1, handicapIndexAndRounded.Item2);
                getGolferResponses.Add(getGolferResponse);
            }

            return getGolferResponses;
        }
    }
}
