namespace GolfHandicap.Features.Matches.Post.GolfMatches
{
    public class GolfMatchDto
    {
        public int GolferId { get; set; }
        public int MatchScheduleId { get; set; }
        public bool Blind { get; set; } = false;
    }
}
