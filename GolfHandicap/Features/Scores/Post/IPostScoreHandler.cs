namespace GolfHandicap.Features.Scores.Post
{
    public interface IPostScoreHandler
    {
        Task<PostScoreResposne> CreateScore(PostScoreRequest request);
        Task UpdateScore(PostScoreRequest request);
    }
}
