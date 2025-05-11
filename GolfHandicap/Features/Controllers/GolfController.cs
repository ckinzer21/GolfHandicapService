using GolfHandicap.Features.Golfers.Get;
using GolfHandicap.Features.Golfers.Post;
using Microsoft.AspNetCore.Mvc;

namespace GolfHandicap.Features.Controllers
{
    [ApiController]
    [Route("api/golfer")]
    public class GolfController : ControllerBase
    {
        private readonly IGetGolferHandler _getGolferHandler;
        private readonly IPostGolferHandler _postGolferHandler;

        public GolfController(IGetGolferHandler getGolferHandler, IPostGolferHandler postGolferHandler)
        {
            _getGolferHandler = getGolferHandler;
            _postGolferHandler = postGolferHandler;
        }

        [HttpGet("{golferId}", Name = "GetGolferById")]
        public async Task<IActionResult> GetGolferById(int golferId)
        {
            if (golferId <= 0) return BadRequest("golferId is required to get the golfer");

            var golfer = await _getGolferHandler.GetGolferById(golferId);
            return golfer != null ? Ok(golfer) : NotFound();
        }

        [HttpGet("GetAllGolfers")]
        public async Task<IActionResult> GetAllGolfers()
        {
            var golfers = await _getGolferHandler.GetAllGolfers();
            return golfers != null ? Ok(golfers) : NotFound();
        }

        [HttpPost("CreateGolfer")]
        public async Task<IActionResult> CreateGolfer([FromBody] PostGolferRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var golfer = await _postGolferHandler.CreateGolfer(request);
            return CreatedAtAction(nameof(CreateGolfer), new { golfer.id }, golfer);
        }

        [HttpPost("CreateGolfers")]
        public async Task<IActionResult> CreateGolfers([FromBody] IEnumerable<PostGolferRequest> requests)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var golfers = await _postGolferHandler.CreateGolfers(requests);
            return Ok(golfers);
        }

        [HttpPost("updategolfer")]
        public async Task<IActionResult> UpdateGolfer([FromBody] PostGolferRequest request)
        {
            if (ModelState.IsValid) return BadRequest(ModelState);
            var result = await _postGolferHandler.UpdateGolfer(request);

            if (!result.Success) return NotFound(result.ErrorMessage);
            return Ok(result);
        }
    }
}
