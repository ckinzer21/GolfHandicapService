namespace GolfHandicap.Features.Golfers.Post
{
    public interface IPostGolferHandler
    {
        public Task<PostGolferResponse> CreateGolfer(CreateGolferRequest request);
        public Task UpdateGolfer(UpdateGolferRequest request);
        Task<IEnumerable<PostGolferResponse>> CreateGolfers(IEnumerable<CreateGolferRequest> requests);
    }
}
