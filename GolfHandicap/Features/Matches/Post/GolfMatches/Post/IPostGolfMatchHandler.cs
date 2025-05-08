namespace GolfHandicap.Features.Matches.Post.GolfMatches.Post
{
    public interface IPostGolfMatchHandler
    {
        Task CreateGolfMatch(IEnumerable<GolfMatchDto> dto);
    }
}
