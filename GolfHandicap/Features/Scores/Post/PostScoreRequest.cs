using GolfHandicap.Entities;

namespace GolfHandicap.Features.Scores.Post
{
    public record PostScoreRequest
    {
        public int ScoreId { get; init; }
        public int Strokes { get; set; }
        public int AdjustedStrokes { get; set; }
        public int Points { get; set; }
        public bool IsDeleted { get; set; }
        public int GolferId { get; init; }
        public int MatchScheduleId { get; init; }
        public int TeeId { get; set; }
    }
}
