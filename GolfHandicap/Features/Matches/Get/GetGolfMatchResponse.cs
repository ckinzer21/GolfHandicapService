using GolfHandicap.Common.Mappings;
using GolfHandicap.Entities;

namespace GolfHandicap.Features.Matches.Get
{
    public class GetGolfMatchResponse : IMapFrom<GolfOpponent>
    {
        public int GolferId { get; set; }
        public int? OpponentGolferId { get; set; }
        public int MatchScheduleId { get; set; }
        public string? Name { get; set; }
        public string? OpponentName { get; set; }
        public int Week { get; set; }
        public DateTime Date { get; set; }
        public double? HandicapIndex { get; set; }
        public int? RoundedHandicap { get; set; }
        public double? OpponentHandicapIndex { get; set; }
        public int? OpponentRoundedHandicap { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
