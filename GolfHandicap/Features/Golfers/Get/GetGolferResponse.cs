namespace GolfHandicap.Features.Golfers.Get
{
    public record GetGolferResponse(int golferId, string name, string email, double? handicapIndex, int? roundedHandicap);
}
