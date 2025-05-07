namespace GolfHandicap.Features.Golfers.Get
{
    public record GetGolferResponse(int golferId, string name, string email, bool isDeleted);
}
