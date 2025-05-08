namespace GolfHandicap.Entities
{
    public class TeeLookup
    {
        public int TeeLookupId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public Course Course { get; set; } = new Course();
        public double CourseRating { get; set; }
        public double Slope { get; set; }
    }
}
