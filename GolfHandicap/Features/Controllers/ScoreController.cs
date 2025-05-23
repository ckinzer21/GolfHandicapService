﻿using GolfHandicap.Features.Scores.Get;
using GolfHandicap.Features.Scores.HoleScores;
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
        private IPostHoleScoreHandler _postHoleScoreHandler { get; set; }

        public ScoreController(IGetScoreHandler getScoreHandler, IPostScoreHandler postScoreHandler, IPostHoleScoreHandler postHoleScoreHandler)
        {
            _getScoreHandler = getScoreHandler;
            _postScoreHandler = postScoreHandler;
            _postHoleScoreHandler = postHoleScoreHandler;
        }

        [HttpGet("GetScoreByScoreId")]
        public async Task<IActionResult> GetScoreByScoreId(int scoreId)
        {
            if (scoreId <= 0) return BadRequest("scoreId is required to get the score");
            var score = await _getScoreHandler.GetScoreByScoreId(scoreId);
            return score != null ? Ok(score) : NotFound();
        }

        [HttpGet("GetScoreByGolferId")]
        public async Task<IActionResult> GetScoreByGolferId(int golferId)
        {
            if (golferId <= 0) return BadRequest("golferId is required to get the scores");
            var scores = await _getScoreHandler.GetScoresByGolferId(golferId);
            return scores != null ? Ok(scores) : NotFound();
        }

        [HttpPost("CreateScore")]
        public async Task<IActionResult> CreateScore([FromBody] PostScoreRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var handicap = await _postScoreHandler.CreateScore(request);

            if (!string.IsNullOrEmpty(handicap.Error)) return NotFound(handicap.Error);

            return Ok(handicap);
        }

        [HttpPost("UpdateScore")]
        public async Task<IActionResult> UpdateScore([FromBody] PostScoreRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var handicap = await _postScoreHandler.UpdateScore(request);

            if (!string.IsNullOrEmpty(handicap.Error)) return NotFound(handicap.Error);

            return Ok(handicap);
        }

        [HttpPost("CreateHolesScore")]
        public async Task<IActionResult> CreateHolesScore([FromBody]IEnumerable<PostHoleScoreRequest> requests)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _postHoleScoreHandler.CreateHolesScore(requests);

            if (!result.Success) return BadRequest(result.ErrorMessage);

            return Ok();
        }
    }
}
