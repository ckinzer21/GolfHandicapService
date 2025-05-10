using GolfHandicap.Features.Majors;
using Microsoft.AspNetCore.Mvc;

namespace GolfHandicap.Features.Controllers
{
    [ApiController]
    [Route("api/major")]
    public class MajorController : ControllerBase
    {
        private readonly IPostMajor _postMajor;

        public MajorController(IPostMajor postMajor)
        {
            _postMajor = postMajor;
        }


        [HttpPost("create")]
        public IActionResult CreateMajors(IEnumerable<PostMajorRequest> requests)
        {
            _postMajor.CreateMajors(requests);
            return Ok();
        }
    }
}
