namespace GolfHandicap.Features.Golfers.Post
{
    public interface IPostGolferHandler
    {
        public Task<PostGolferResponse> CreateGolfer(string name, string email);
        public Task UpdateGolfer(int golferId, string name, string email, bool isDeleted);
    }
}
