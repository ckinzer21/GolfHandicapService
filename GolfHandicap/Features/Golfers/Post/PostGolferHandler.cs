using GolfHandicap.Data;
using GolfHandicap.Entities;
using Microsoft.EntityFrameworkCore;

namespace GolfHandicap.Features.Golfers.Post
{
    public class PostGolferHandler : IPostGolferHandler
    {
        private readonly DataContext _context;

        public PostGolferHandler(DataContext context) => _context = context;

        public async Task<PostGolferResponse> CreateGolfer(CreateGolferRequest request)
        {
            var golfer = new Golfer { Name = request.name, Email = request.email };

            _context.Add(golfer);
            await _context.SaveChangesAsync();

            return new PostGolferResponse(golfer.GolferId, golfer.Name, golfer.Email);
        }

        public async Task UpdateGolfer(UpdateGolferRequest request)
        {
            var golfer = await _context.Golfers.FirstOrDefaultAsync(g => g.GolferId == request.golferId);

            if (golfer == null) return;// not good, need to message that something failed or that there was no golfer to update

            golfer.Name = request.name;
            golfer.Email = request.email;
            golfer.IsDeleted = request.isDeleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostGolferResponse>> CreateGolfers(IEnumerable<CreateGolferRequest> requests)
        {
            List<PostGolferResponse> responses = new List<PostGolferResponse>();

            foreach(var request in requests)
            {
                var response = await CreateGolfer(request);
                responses.Add(response);
            }

            return responses;
        }
    }
}
