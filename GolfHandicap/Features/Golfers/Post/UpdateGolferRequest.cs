namespace GolfHandicap.Features.Golfers.Post
{
    public record UpdateGolferRequest
    {
        public int GolferId { get; set; }
        public string? Name { get; init; }
        public string? Email { get; init; }
        public int FlightId { get; init; }
        public bool IsDeleted { get; init; }
    }
}
