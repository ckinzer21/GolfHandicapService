using GolfHandicap.Features.Matches.Post.GolfMatches.Preview;
using GolfHandicap.Features.Matches.Post.GolfMatches;
using GolfHandicap.Features.Matches.Post.Schedule;
using Microsoft.AspNetCore.Mvc;

namespace GolfHandicap.Features.Controller
{
    [ApiController]
    [Route("api/match")]
    public class MatchController : ControllerBase
    {
        private readonly IPostMatchScheduleHandler _postHandler;
        private readonly IPreviewGolfMatchHandler _previewHandler;

        public MatchController(IPostMatchScheduleHandler postHandler, IPreviewGolfMatchHandler previewHandler)
        {
            _postHandler = postHandler;
            _previewHandler = previewHandler;
        }

        [HttpPost("yearly", Name = "CreateYearlyMatchSchedule")]
        public async Task<IActionResult> CreateYearlyMatchSchedule(IEnumerable<PostMatchScheduleRequest> requests)
        {
            await _postHandler.CreateYearlySchedule(requests);
            return Ok();// not good, need to handle errors
        }

        [HttpGet("preview", Name = "PreviewGolfMatches")]
        public async Task<IEnumerable<GolfMatchResponse>> PreviewGolfMatches()
        {
            await _previewHandler.PreivewSchedule();
            return new List<GolfMatchResponse>();
        }
    }
}
