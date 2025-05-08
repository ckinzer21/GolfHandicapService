namespace GolfHandicap.Entities
{
    public class Golfer
    {
        public int GolferId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public ICollection<Handicap> CourseHandicaps { get; set; } = new List<Handicap>();
        public ICollection<GolfMatch> GolfMatches { get; set; } = new List<GolfMatch>();
        public ICollection<Score> Scores { get; set; } = new List<Score>();
        public FlightLookup FlightLookup { get; set; } = new FlightLookup();
    }
}
