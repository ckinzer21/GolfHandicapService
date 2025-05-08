namespace GolfHandicap.Features.Scores.Get
{
    public record GetScoreResponse(int scoreId, int grossStrokes, int adjustedGrossStrokes, int matchId, int golferId, int courseId);
}
