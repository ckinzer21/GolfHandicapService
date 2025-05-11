namespace GolfHandicap.Features.Golfers.Post
{
    public class PostGolferRequest
    {
        public int? GolferId { get; init; }
        public string? Name { get; set; }
        public string? Email { get; init; }
        public int? FlightId { get; init; }
        public bool IsDeleted { get; set; }
    }
}
