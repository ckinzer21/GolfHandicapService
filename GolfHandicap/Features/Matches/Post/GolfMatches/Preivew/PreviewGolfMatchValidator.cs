namespace GolfHandicap.Features.Matches.Post.GolfMatches.Preview
{
    public interface IPreviewGolfMatchValidator
    {
        bool ScheduleValidator(int year);
    }

    public class PreviewGolfMatchValidator : IPreviewGolfMatchValidator
    {
        public bool ScheduleValidator(int year)
        {
            if (year == int.MinValue) return false;
            if (year == DateTime.Today.Year) return false;
            return true;
        }
    }
}
