using System.Text.Json.Serialization;

namespace GolfHandicap.Entities
{
    public class Golfer
    {
        public int GolferId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public bool IsDeleted { get; set; }
        public int? GolfMatchId { get; set; }
        public int? ScoreId { get; set; }
        public int? FlightId { get; set; }
        public ICollection<GolfMatch>? GolfMatches { get; set; }
        public ICollection<Score>? Scores { get; set; }
        public Flight? Flight { get; set; }
    }
}
