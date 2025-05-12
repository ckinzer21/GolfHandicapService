namespace GolfHandicap.Features.Setup.StartOfYear
{
    public interface IStartOfYearHandler
    {
        Task<IEnumerable<GolferStartOfYearResponse>> SetupStartOfYearPreview();
        Task<IEnumerable<GolferStartOfYearResponse>> SetupStartOfYear();
    }
}
