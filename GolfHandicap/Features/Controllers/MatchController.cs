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

        [HttpPost("schedule")]
        public async Task<IActionResult> CreateSchedule([FromBody] ScheduleRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _postHandler.CreateScheduleByFlight(request);
            return Ok();
        }
    }
}
