using GolfHandicap.Features.Matches.Post.Schedule;
using Microsoft.AspNetCore.Mvc;

namespace GolfHandicap.Features.Controller
{
    [ApiController]
    [Route("api/match")]
    public class MatchController : ControllerBase
    {
        private readonly IPostMatchScheduleHandler _postHandler;

        public MatchController(IPostMatchScheduleHandler postHandler)
        {
            _postHandler = postHandler;
        }

        [HttpPost("yearly", Name = "CreateYearlyMatchSchedule")]
        public async Task<IActionResult> CreateYearlyMatchSchedule([FromBody] IEnumerable<PostMatchScheduleRequest> requests)
        {
            await _postHandler.CreateYearlySchedule(requests);
            return Ok();// not good, need to handle errors
        }
    }
}
