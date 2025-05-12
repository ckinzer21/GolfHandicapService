using GolfHandicap.Common;
using GolfHandicap.Data;
using GolfHandicap.Entities;

namespace GolfHandicap.Features.Scores.HoleScores
{
    public class PostHoleScoreHandler : IPostHoleScoreHandler
    {
        private readonly DataContext _context;

        public PostHoleScoreHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<OperationResult> CreateHolesScore(IEnumerable<PostHoleScoreRequest> requests)
        {
            foreach (var request in requests)
            {
                if (request.ScoreId <= 0 || request.Par <= 0 || request.HoleNumber <= 0 || request.Strokes <= 0)
                    return OperationResult.Fail("Missing required field");
                
                var holeScore = new HoleScore()
                {
                    HoleNumber = request.HoleNumber,
                    Par = request.Par,
                    Strokes = request.Strokes,
                    ScoreId = request.ScoreId,
                    Handicap = request.Handicap,
                };

                _context.Add(holeScore);
            }

            await _context.SaveChangesAsync();
            return OperationResult.Ok();
        }

        public async Task<OperationResult> UpdateHolesScore(PostHoleScoreRequest request)
        {
            if (request.ScoreId <= 0 || request.Par <= 0 || request.HoleNumber <= 0 || request.Strokes <= 0)
                return OperationResult.Fail("Missing required field");

            var holeScore = new HoleScore()
            {
                HoleNumber = request.HoleNumber,
                Par = request.Par,
                Strokes = request.Strokes,
                ScoreId = request.ScoreId,
                Handicap = request.Handicap,
            };

            _context.Add(holeScore);

            await _context.SaveChangesAsync();
            return OperationResult.Ok();
        }
    }
}
