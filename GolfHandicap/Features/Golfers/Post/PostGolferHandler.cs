using GolfHandicap.Common;
using GolfHandicap.Data;
using GolfHandicap.Entities;
using Microsoft.EntityFrameworkCore;

namespace GolfHandicap.Features.Golfers.Post
{
    public class PostGolferHandler : IPostGolferHandler
    {
        private readonly DataContext _context;

        public PostGolferHandler(DataContext context) => _context = context;

        public async Task<PostGolferResponse> CreateGolfer(PostGolferRequest request)
        {
            var golfer = new Golfer { Name = request.Name, Email = request.Email, FlightId = request.FlightId };

            _context.Add(golfer);
            await _context.SaveChangesAsync();

            return new PostGolferResponse(golfer.GolferId, golfer.Name, golfer.Email);
        }

        public async Task<OperationResult> UpdateGolfer(PostGolferRequest request)
        {
            var golfer = await _context.Golfer.FirstOrDefaultAsync(g => g.GolferId == request.GolferId);

            if (golfer == null) return OperationResult.Fail("No golfer found");

            golfer.Name = request.Name;
            golfer.Email = request.Email;
            golfer.FlightId = request.FlightId;
            golfer.IsDeleted = request.IsDeleted;
            await _context.SaveChangesAsync();

            return OperationResult.Ok();
        }

        public async Task<IEnumerable<PostGolferResponse>> CreateGolfers(IEnumerable<PostGolferRequest> requests)
        {
            List<PostGolferResponse> responses = [];

            foreach(var request in requests)
            {
                var response = await CreateGolfer(request);
                responses.Add(response);
            }

            return responses;
        }
    }
}
