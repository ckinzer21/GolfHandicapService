namespace GolfHandicap.Common.Handicaps
{
    public interface IGetHandicap
    {
        Task<HandicapIndexResult> GetIndexAndRounded(int? golferId, bool isStartOfYear = false);
    }
}
