using GolfHandicap.Features.Scores.Get;
using GolfHandicap.Features.Scores.Post;
using Microsoft.AspNetCore.Mvc;

namespace GolfHandicap.Features.Controllers
{
    [ApiController]
    [Route("api/score")]
    public class ScoreController : ControllerBase
    {
        private IGetScoreHandler _getScoreHandler { get; set; }
        private IPostScoreHandler _postScoreHandler { get; set; }

        public ScoreController(IGetScoreHandler getScoreHandler, IPostScoreHandler postScoreHandler)
        {
            _getScoreHandler = getScoreHandler;
            _postScoreHandler = postScoreHandler;
        }

        [HttpGet("GetScoreByScoreId")]
        public async Task<IActionResult> GetScoreByScoreId(int scoreId)
        {
            var score = await _getScoreHandler.GetScoreByScoreId(scoreId);
            return score != null ? Ok(score) : NotFound();
        }

        [HttpGet("GetScoreByGolferId")]
        public async Task<IActionResult> GetScoreByGolferId(int golferId)
        {
            var scores = await _getScoreHandler.GetScoresByGolferId(golferId);
            return scores != null ? Ok(scores) : NotFound();
        }

        [HttpPost("CreateScore")]
        public async Task<IActionResult> CreateScore([FromBody] PostScoreRequest request)
        {
            var handicap = await _postScoreHandler.CreateScore(request);

            if (!string.IsNullOrEmpty(handicap.Error)) return NotFound(handicap.Error);

            return Ok(handicap);
        }

        [HttpPost("UpdateScore")]
        public async Task<IActionResult> UpdateScore([FromBody] PostScoreRequest request)
        {
            var handicap = await _postScoreHandler.UpdateScore(request);

            if (!string.IsNullOrEmpty(handicap.Error)) return NotFound(handicap.Error);

            return Ok(handicap);
        }

        [HttpGet("Handicap")]
        public async Task<IActionResult> GetHandicapIndex(int golferId)
        {
            if (golferId <= 0) return BadRequest("golferId is required to get the handicap");
            var handicap = await _getScoreHandler.GetHandicapIndex(golferId);

            if (!string.IsNullOrEmpty(handicap.Error)) return BadRequest(handicap.Error);

            return Ok(handicap);
        }
    }
}
