using GolfHandicap.Common.Mappings;
using GolfHandicap.Entities;

namespace GolfHandicap.Features.Golfers.Get
{
    public record GetGolferResponse : IMapFrom<Golfer>
    {
        public int GolferId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public double? HandicapIndex { get; set; }
        public int? RoundedHandicap { get; set; }
    }
}
