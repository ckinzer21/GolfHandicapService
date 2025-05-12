namespace GolfHandicap.Features.Setup.StartOfYear
{
    public record GolferStartOfYearResponse
    {
        public int GolferId { get; set; }
        public string? Name { get; set; }
        public double? HandicapIndex { get; set; }
        public string? Flight { get; set; }
        public int FlightId { get; set; }
    }
}
