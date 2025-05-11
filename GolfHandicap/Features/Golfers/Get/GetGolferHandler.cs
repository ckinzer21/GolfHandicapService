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
            var golfer = await _context.Golfer.FirstOrDefaultAsync(g => g.GolferId == golferId);
            if (golfer == null) throw new Exception("No Golfer Found");
            HandicapIndexResult handicapIndexAndRounded = await _getHandicap.GetIndexAndRounded(golfer.GolferId);
            return new GetGolferResponse(golfer.GolferId, golfer.Name, golfer.Email, handicapIndexAndRounded.HandicapIndex, handicapIndexAndRounded.RoundedHandicap);
        }

        public async Task<IEnumerable<GetGolferResponse?>> GetAllGolfers()
        {
            var golfers = await _context.Golfer.ToListAsync();
            if (golfers.Count == 0) throw new Exception("No Golfers Found");

            List<GetGolferResponse> getGolferResponses = [];

            foreach (var golfer in golfers)
            {
                HandicapIndexResult handicapIndexAndRounded = await _getHandicap.GetIndexAndRounded(golfer.GolferId);
                var getGolferResponse = new GetGolferResponse(golfer.GolferId, golfer.Name, golfer.Email, handicapIndexAndRounded.HandicapIndex, handicapIndexAndRounded.RoundedHandicap);
                getGolferResponses.Add(getGolferResponse);
            }

            return getGolferResponses;
        }
    }
}
