namespace GolfHandicap.Entities
{
    public class HoleScore
    {
        public int HoleScoreId { get; set; }
        public int HoleNumber { get; set; }
        public int Par { get; set; }
        public int StrokesReceived { get; set; }
        public int ActualStrokes { get; set; }
        public Score? Score { get; set; }
    }
}
