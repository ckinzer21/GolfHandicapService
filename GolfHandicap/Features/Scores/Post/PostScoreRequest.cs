namespace GolfHandicap.Features.Scores.Post
{
    public record PostScoreRequest
    {
        public int ScoreId { get; init; }
        public int Strokes { get; init; }
        public int AdjustedStrokes { get; init; }
        public int GolferId { get; init; }
        public int MatchScheduleId { get; init; }
    }
}
