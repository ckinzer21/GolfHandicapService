namespace GolfHandicap.Entities
{
    public class Major
    {
        public int MajorId { get; set; }
        public string? Name { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<MatchSchedule>? MatchSchedules { get; set; }
    }
}
