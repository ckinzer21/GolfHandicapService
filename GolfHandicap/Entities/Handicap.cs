namespace GolfHandicap.Entities
{
    public class Handicap
    {
        public int HandicapId { get; set; }
        public decimal CourseHandicap { get; set; }
        public Golfer Golfer { get; set; } = new Golfer();
    }
}
