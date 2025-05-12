using GolfHandicap.Entities;
using GolfHandicap.Features.Setup;
using GolfHandicap.Features.Setup.Courses;
using GolfHandicap.Features.Setup.Flights;
using GolfHandicap.Features.Setup.Majors;
using GolfHandicap.Features.Setup.StartOfYear;
using Microsoft.AspNetCore.Mvc;

namespace GolfHandicap.Features.Controllers
{
    [ApiController]
    [Route("api/setup")]
    public class SetupController : ControllerBase
    {
        private readonly IPostSetupHandler _postSetupHandler;
        private readonly IStartOfYearHandler _startOfYearHandler;

        public SetupController(IPostSetupHandler postSetupHandler)
        {
            _postSetupHandler = postSetupHandler;
        }

        [HttpPost("createcourse")]
        public async Task<IActionResult> CreateCourseAndTees([FromBody] IEnumerable<CreateCourseRequest> requests)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _postSetupHandler.CreateCourseAndTees(requests);

            return Ok();
        }

        [HttpPost("createmajor")]
        public async Task<IActionResult> CreateMajors(IEnumerable<PostMajorRequest> requests)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _postSetupHandler.CreateMajors(requests);
            return Ok();
        }

        [HttpPost("createflight")]
        public async Task<IActionResult> CreateFlight(IEnumerable<PostFlightRequest> requests)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _postSetupHandler.CreateFlight(requests);
            return Ok();
        }

        [HttpPost("createweight")]
        public async Task<IActionResult> CreateWeight(double pct)
        {
            if (pct <= 0.0) return BadRequest("pct is required to create the weight");
            await _postSetupHandler.CreateWeight(pct);
            return Ok();
        }

        [HttpGet("previewstartofyear")]
        public async Task<IActionResult> PreviewStartOfYear()
        {
            var result = await _startOfYearHandler.SetupStartOfYearPreview();
            return Ok(result);
        }
    }
}
