namespace GolfHandicap.Features.Setup.Courses
{
    public record CreateTeeLookupRequest
    {
        public string TeeName { get; set; } = string.Empty;
        public double CourseRating { get; set; }
        public double Slope { get; set; }
    }
}
