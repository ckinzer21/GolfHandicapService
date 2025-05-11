using GolfHandicap.Common;
using GolfHandicap.Entities;

namespace GolfHandicap.Features.Scores.Post
{
    public interface IPostScoreHandler
    {
        Task<HandicapIndexResult> CreateScore(PostScoreRequest request);
        Task<HandicapIndexResult> UpdateScore(PostScoreRequest request);
    }
}
