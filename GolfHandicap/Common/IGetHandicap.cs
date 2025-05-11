namespace GolfHandicap.Common
{
    public interface IGetHandicap
    {
        Task<HandicapIndexResult> GetIndexAndRounded(int golferId);
    }
}
