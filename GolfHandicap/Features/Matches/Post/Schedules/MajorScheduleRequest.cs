namespace GolfHandicap.Features.Matches.Post.Schedules
{
    public record MajorScheduleRequest
    {
        public DateTime Date { get; set; }
        public int MajorId { get; set; }
    }
}
