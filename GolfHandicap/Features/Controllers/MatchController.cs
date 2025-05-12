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

        [HttpGet("getgolfmatchesbyyear")]
        public async Task<IActionResult> GetGolfMatchesByYear(int year)
        {
            if (year <= 0) return BadRequest("year must be supplied");
            var result = await _getHandler.GetGolfMatchByYear(year);
            return Ok(result);
        }

        [HttpGet("getgolfmatchesbyyearandweek")]
        public async Task<IActionResult> GetGolfMatchesByYearAndWeek(GetGolfMatchRequest request)
        {
            if (request.Year <= 0 || request.Week <= 0) return BadRequest("year and week must be supplied");
            var result =  await _getHandler.GetGolfMatchByYearAndWeek(request.Year, request.Week);
            return Ok(result);
        }
    }
}
