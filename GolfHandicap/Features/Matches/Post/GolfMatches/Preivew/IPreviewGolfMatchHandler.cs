using GolfHandicap.Features.Controller;

namespace GolfHandicap.Features.Matches.Post.GolfMatches.Preview
{
    public interface IPreviewGolfMatchHandler
    {
        Task<IEnumerable<GolfMatchResponse>> PreivewSchedule();
    }
}
