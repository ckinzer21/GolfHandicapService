namespace GolfHandicap.Entities
{
    public class GolfMatch
    {
        public int GolferId { get; set; }
        public int MatchScheduleId { get; set; }
        public Golfer? Golfer { get; set; }
        public MatchSchedule? MatchSchedule { get; set; }
    }
}
