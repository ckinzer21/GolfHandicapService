namespace GolfHandicap.Features.Scores.Get
{
    public interface IGetScoreHandler
    {
        Task<GetScoreResponse?> GetScoreByScoreId(int id);
        Task<IEnumerable<GetScoreResponse?>> GetScoresByGolferId(int golferId);
    }
}
