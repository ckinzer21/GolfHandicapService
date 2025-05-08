using GolfHandicap.Data;
using GolfHandicap.Entities;

namespace GolfHandicap.Features.Courses
{
    public interface ICreateCourseHandler
    {
        Task Create(CreateCourseRequest request);
    }

    public class CreateCourseHandler : ICreateCourseHandler
    {
        private readonly DataContext _context;

        public CreateCourseHandler(DataContext context)
        {
            _context = context;
        }

        public async Task Create(CreateCourseRequest request)
        {
            var course = new Course
            {
                Name = request.Name,
                Tees = request.TeeLookupRequest.Select(t => new TeeLookup
                {
                    Name = t.TeeName,
                    CourseRating = t.CourseRating,
                    Slope = t.Slope
                }).ToList()
            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }
    }
}
