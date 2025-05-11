using GolfHandicap.Features.Matches.Get;
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
        private readonly IGetGolfMatchHandler _getHandler;

        public MatchController(IPostMatchScheduleHandler postHandler, IGetGolfMatchHandler getHandler)
        {
            _postHandler = postHandler;
            _getHandler = getHandler;
        }

        [HttpPost("schedule")]
        public async Task<IActionResult> CreateSchedule([FromBody] ScheduleRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _postHandler.CreateScheduleByFlight(request);
            return Ok();
        }

        [HttpGet("getgolfmatches")]
        public async Task<IActionResult> GetGolfMatches(int year)
        {
            if (year <= 0) return BadRequest("year must be supplied");
            var result = await _getHandler.GetGolfMatch(year);
            return Ok(result);
        }
    }
}
