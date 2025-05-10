namespace GolfHandicap.Features.Golfers.Post
{
    public record CreateGolferRequest
    {
        public string? Name { get; set; }
        public string? Email { get; init; }
        public int? FlightId { get; init; }
    }
}
