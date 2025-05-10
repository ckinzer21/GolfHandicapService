using GolfHandicap.Features.Flights;
using Microsoft.AspNetCore.Mvc;

namespace GolfHandicap.Features.Controllers
{
    [ApiController]
    [Route("api/flight")]
    public class FlightController : ControllerBase
    {
        private readonly IPostFlight _postFlight;
        public FlightController(IPostFlight postFlight)
        {
            _postFlight = postFlight;
        }

        [HttpPost("create")]
        public IActionResult CreateFlight(IEnumerable<PostFlightRequest> requests)
        {
            _postFlight.CreateFlight(requests);
            return Ok();
        }
    }
}
