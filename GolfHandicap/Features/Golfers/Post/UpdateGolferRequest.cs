namespace GolfHandicap.Features.Golfers.Post
{
    public record UpdateGolferRequest(int golferId, string name, string email, bool isDeleted);
}
