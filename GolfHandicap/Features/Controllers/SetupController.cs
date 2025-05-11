using GolfHandicap.Features.Setup;
using GolfHandicap.Features.Setup.Courses;
using GolfHandicap.Features.Setup.Flights;
using GolfHandicap.Features.Setup.Majors;
using Microsoft.AspNetCore.Mvc;

namespace GolfHandicap.Features.Controllers
{
    [ApiController]
    [Route("api/setup")]
    public class SetupController : ControllerBase
    {
        private readonly IPostSetupHandler _postSetupHandler;

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
        public IActionResult CreateMajors(IEnumerable<PostMajorRequest> requests)
        {
            _postSetupHandler.CreateMajors(requests);
            return Ok();
        }

        [HttpPost("createflight")]
        public IActionResult CreateFlight(IEnumerable<PostFlightRequest> requests)
        {
            _postSetupHandler.CreateFlight(requests);
            return Ok();
        }

        [HttpPost("createweight")]
        public IActionResult CreateWeight(double pct)
        {
            _postSetupHandler.CreateWeight(pct);
            return Ok();
        }
    }
}
