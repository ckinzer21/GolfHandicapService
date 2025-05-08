namespace GolfHandicap.Entities
{
    public class MatchSchedule
    {
        public int MatchScheduleId { get; set; }
        public int? Week { get; set; }
        public DateTime Date { get; set; }
        public int? MajorId { get; set; }
        public Major? Major { get; set; }
        public bool Blind { get; set; } = false;
        public ICollection<GolfMatch>? GolfMatches { get; set; }

    }
}
