namespace GolfHandicap.Entities
{
    public class MatchSchedule
    {
        public int MatchScheduleId { get; set; }
        public int Year { get; set; }
        public int Week { get; set; }
        public bool Blind { get; set; } = false;
        public ICollection<GolfMatch> GolfMatches { get; set; } = new List<GolfMatch>();
    }
}
