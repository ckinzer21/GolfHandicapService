using GolfHandicap.Common;

namespace GolfHandicap.Features.Golfers.Post
{
    public interface IPostGolferHandler
    {
        public Task<PostGolferResponse> CreateGolfer(PostGolferRequest request);
        public Task<OperationResult> UpdateGolfer(PostGolferRequest request);
        Task<IEnumerable<PostGolferResponse>> CreateGolfers(IEnumerable<PostGolferRequest> requests);
    }
}
