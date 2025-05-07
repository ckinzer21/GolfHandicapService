namespace GolfHandicap.Entities
{
    public class Score
    {
        public int ScoreId { get; set; }
        public int Strokes { get; set; }
        public int AdjustedStrokes { get; set; }
        public MatchSchedule Match { get; set; } = new MatchSchedule();
        public Golfer Golfer { get; set; } = new Golfer();
    }
}
