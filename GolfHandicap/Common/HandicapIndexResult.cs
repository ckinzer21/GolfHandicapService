namespace GolfHandicap.Common
{
    public record HandicapIndexResult
    {
        public double? HandicapIndex { get; set; }
        public int? RoundedHandicap { get; set; }
        public string? Error { get; set; }
    }
}
