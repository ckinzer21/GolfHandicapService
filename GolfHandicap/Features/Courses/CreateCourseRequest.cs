namespace GolfHandicap.Features.Courses
{
    public record CreateCourseRequest
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<CreateTeeLookupRequest> TeeLookupRequest { get; set; } = new List<CreateTeeLookupRequest>();
    }
}
