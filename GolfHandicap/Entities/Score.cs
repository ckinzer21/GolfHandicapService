namespace GolfHandicap.Entities
{
    public class Score
    {
        public int ScoreId { get; set; }
        public int GrossStrokes { get; set; }
        public int AdjustedGrossStrokes { get; set; }
        public int MatchScheduleId { get; set; }
        public int GolferId { get; set; }
        public int TeeId { get; set; }
        public MatchSchedule? MatchSchedule { get; set; }
        public Golfer? Golfer { get; set; }
        public ICollection<HoleScore>? HolesScore { get; set; }
        public Tee? Tee { get; set; }
    }
}
