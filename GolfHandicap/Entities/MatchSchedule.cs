namespace GolfHandicap.Entities
{
    public class MatchSchedule
    {
        public int MatchScheduleId { get; set; }
        public int Year { get; set; }
        public int Week { get; set; }
        public ICollection<GolfMatch> GolfMatches { get; set; } = new List<GolfMatch>();
    }
}
