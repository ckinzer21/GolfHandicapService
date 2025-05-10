using GolfHandicap.Features.Matches.Post.Schedule;
using GolfHandicap.Features.Matches.Post.Schedules;
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

        //probably going to remove this
        [HttpPost("yearly", Name = "CreateYearlyMatchSchedule")]
        public async Task<IActionResult> CreateYearlyMatchSchedule([FromBody] IEnumerable<PostMatchScheduleRequest> requests)
        {
            await _postHandler.CreateYearlySchedule(requests);
            return Ok();// not good, need to handle errors
        }

        [HttpPost("schedule")]
        public async Task<IActionResult> CreateSchedule([FromBody] ScheduleRequest request)
        {
            await _postHandler.CreateScheduleByFlight(request);
            return Ok();
        }
    }
}
