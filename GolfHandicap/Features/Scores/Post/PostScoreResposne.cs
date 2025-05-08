namespace GolfHandicap.Features.Scores.Post
{
    public record PostScoreResposne(int scoreId, int strokes, int adjustedStrokes, int golferId, int matchId);
}
