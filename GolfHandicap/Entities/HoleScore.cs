﻿namespace GolfHandicap.Entities
{
    public class HoleScore
    {
        public int HoleScoreId { get; set; }
        public int HoleNumber { get; set; }
        public int Par { get; set; }
        public int Strokes { get; set; }
        public int Handicap { get; set; }
        public bool IsDeleted { get; set; }
        public int ScoreId { get; set; }
        public Score? Score { get; set; }
    }
}
