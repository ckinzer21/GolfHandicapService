namespace GolfHandicap.Features.Scores.HoleScores
{
    public record PostHoleScoreRequest
    {
        public int HoleScoreId { get; set; }
        public int HoleNumber { get; set; }
        public int Par { get; set; }
        public int Strokes { get; set; }
        public int Handicap { get; set; }
        public int ScoreId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
