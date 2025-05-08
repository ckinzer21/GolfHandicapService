namespace GolfHandicap.Entities
{
    public class Score
    {
        public int ScoreId { get; set; }
        public int GrossStrokes { get; set; }
        public int AdjustedGrossStrokes { get; set; }
        public int MatchScheduleId { get; set; }
        public int GolferId { get; set; }
        public int CourseId { get; set; }
        public MatchSchedule MatchSchedule { get; set; } = new MatchSchedule();
        public Golfer Golfer { get; set; } = new Golfer();
        public Course Course { get; set; } = new Course();
        public ICollection<HoleScore> HolesScore { get; set; } = new List<HoleScore>();
    }
}
