namespace GolfHandicap.Entities
{
    public class GolfOpponent
    {
        public int GolferId { get; set; }
        public int? OpponentGolferId { get; set; }
        public int MatchScheduleId { get; set; }
        public string? Name { get; set; }
        public string? OpponentName { get; set; }
        public int Week { get; set; }
        public DateTime Date { get; set; }
    }
}
