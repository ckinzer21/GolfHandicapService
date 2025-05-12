namespace GolfHandicap.Features.Matches.Get
{
    public record GetGolfMatchRequest
    {
        public int Year { get; init; }
        public int Week { get; init; }
    }
}
