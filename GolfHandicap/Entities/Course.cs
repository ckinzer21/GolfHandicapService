namespace GolfHandicap.Entities
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<TeeLookup> Tees { get; set; } = new List<TeeLookup>();
        public ICollection<Score> Scores { get; set; } = new List<Score>();
    }
}
