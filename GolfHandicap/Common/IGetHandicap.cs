namespace GolfHandicap.Common
{
    public interface IGetHandicap
    {
        Task<(double, int)> GetIndexAndRounded(int golferId);
    }
}
