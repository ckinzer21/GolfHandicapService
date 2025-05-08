using GolfHandicap.Data;
using GolfHandicap.Entities;

namespace GolfHandicap.Features.Matches.Post.GolfMatches.Post
{
    public class PostGolfMatchHandler : IPostGolfMatchHandler
    {
        private readonly DataContext _context;

        public PostGolfMatchHandler(DataContext context)
        {
            _context = context;
        }

        public async Task CreateGolfMatch(IEnumerable<GolfMatchDto> golfMatchDtos)
        {
            foreach (var golfMatchDto in golfMatchDtos)
            {
                var golfMatch = new GolfMatch { GolferId = golfMatchDto.GolferId, MatchScheduleId = golfMatchDto.MatchScheduleId };
                _context.Add(golfMatch);

            }
        }
    }
}
