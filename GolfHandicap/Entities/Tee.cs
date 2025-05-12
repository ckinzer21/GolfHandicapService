namespace GolfHandicap.Entities
{
    public class Tee
    {
        public int TeeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public double CourseRating { get; set; }
        public double Slope { get; set; }
        public int CourseId { get; set; }
        public bool IsDeleted { get; set; }
        public Course? Course { get; set; }
    }
}
