using GolfHandicap.Features.Courses;
using Microsoft.AspNetCore.Mvc;

namespace GolfHandicap.Features.Controllers
{
    [ApiController]
    [Route("api/course")]
    public class CourseController : ControllerBase
    {
        private ICreateCourseHandler _createCourseHandler { get; set; }

        public CourseController(ICreateCourseHandler createCourseHandler)
        {
            _createCourseHandler = createCourseHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] IEnumerable<CreateCourseRequest> requests)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _createCourseHandler.Create(requests);

            return Ok();
        }
    }
}
