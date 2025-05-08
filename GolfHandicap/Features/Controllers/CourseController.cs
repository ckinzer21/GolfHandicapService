using GolfHandicap.Entities;
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

        [HttpGet]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _createCourseHandler.Create(request);

            return Ok();
        }
    }
}
