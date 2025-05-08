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
            var golfer = await _getGolferHandler.GetGolferById(golferId);
            return golfer != null ? Ok(golfer) : NotFound();
        }

        [HttpGet("GetAllGolfers")]
        public async Task<IActionResult> GetAllGolfers()
        {
            var golfers = await _getGolferHandler.GetAllGolfers();
            return golfers != null ? Ok(golfers) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string name, string email)
        {
            var golfer = await _postGolferHandler.CreateGolfer(name, email);
            return CreatedAtAction(nameof(Create), new { golfer.id }, golfer);
        }
    }
}
