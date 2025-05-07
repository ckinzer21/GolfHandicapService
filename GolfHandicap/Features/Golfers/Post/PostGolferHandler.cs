using GolfHandicap.Data;
using GolfHandicap.Entities;
using Microsoft.EntityFrameworkCore;

namespace GolfHandicap.Features.Golfers.Post
{
    public class PostGolferHandler : IPostGolferHandler
    {
        private readonly DataContext _context;

        public PostGolferHandler(DataContext context) => _context = context;

        public async Task<PostGolferResponse> CreateGolfer(string name, string email)
        {
            var golfer = new Golfer { Name = name, Email = email };

            _context.Add(golfer);
            await _context.SaveChangesAsync();

            return new PostGolferResponse(golfer.GolferId, golfer.Name, golfer.Email);
        }

        public async Task UpdateGolfer(int golferId, string name, string email, bool isDeleted)
        {
            var golfer = await _context.Golfers.FirstOrDefaultAsync(g => g.GolferId == golferId);

            if (golfer == null) return;// not good, need to message that something failed or that there was no golfer to update

            golfer.Name = name;
            golfer.Email = email;
            golfer.IsDeleted = isDeleted;
            await _context.SaveChangesAsync();
        }
    }
}
