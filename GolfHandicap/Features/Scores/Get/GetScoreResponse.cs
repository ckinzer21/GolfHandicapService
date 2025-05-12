using GolfHandicap.Common.Mappings;
using GolfHandicap.Entities;

namespace GolfHandicap.Features.Scores.Get
{
    public record GetScoreResponse : IMapFrom<Score>
    {
        public int ScoreId { get; set; }
        public int GrossStrokes { get; set; }
        public int AdjustedGrossStrokes { get; set; }
        public int MatchId { get; set; }
        public int GolferId { get; set; }
        public int TeeId { get; set; }
    }
}
