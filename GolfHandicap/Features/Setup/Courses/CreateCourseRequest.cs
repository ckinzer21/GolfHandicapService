namespace GolfHandicap.Features.Setup.Courses
{
    public record CreateCourseRequest
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<CreateTeeLookupRequest> Tees { get; set; } = new List<CreateTeeLookupRequest>();
    }
}
