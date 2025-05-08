using System.Text.Json.Serialization;

namespace GolfHandicap.Entities
{
    public class Golfer
    {
        public int GolferId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public ICollection<GolfMatch>? GolfMatches { get; set; }
        public ICollection<Score>? Scores { get; set; }
        public FlightLookup ?FlightLookup { get; set; }
    }
}
