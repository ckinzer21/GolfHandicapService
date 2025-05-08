namespace GolfHandicap.Entities
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double CourseRating { get; set; }
        public double Slope { get; set; }
        public string Tees { get; set; } = string.Empty;
        public ICollection<Score> Scores { get; set; } = new List<Score>();
    }
}
