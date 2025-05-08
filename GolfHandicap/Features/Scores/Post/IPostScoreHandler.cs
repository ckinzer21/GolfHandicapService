using GolfHandicap.Entities;

namespace GolfHandicap.Features.Scores.Post
{
    public interface IPostScoreHandler
    {
        Task<(double, int)> CreateScore(PostScoreRequest request);
        Task<(double, int)> UpdateScore(PostScoreRequest request);
    }
}
