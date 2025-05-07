namespace GolfHandicap.Entities
{
    public class GolfMatch
    {
        public int GolferId { get; set; }
        public int MatchScheduleId { get; set; }
        public Golfer Golfer { get; set; } = new Golfer();
        public MatchSchedule MatchSchedule { get; set; } = new MatchSchedule();
    }
}
