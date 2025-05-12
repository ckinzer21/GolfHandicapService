using GolfHandicap.Common;

namespace GolfHandicap.Features.Scores.HoleScores
{
    public interface IPostHoleScoreHandler
    {
        Task<OperationResult> CreateHolesScore(IEnumerable<PostHoleScoreRequest> request);
        Task<OperationResult> UpdateHolesScore(PostHoleScoreRequest request);
    }
}
