using GolfHandicap.Data;
using GolfHandicap.Entities;

namespace GolfHandicap.Features.Courses
{
    public class CreateCourseHandler : ICreateCourseHandler
    {
        private readonly DataContext _context;

        public CreateCourseHandler(DataContext context)
        {
            _context = context;
        }

        public async Task Create(IEnumerable<CreateCourseRequest> requests)
        {
            foreach(var request in requests)
            {
                var course = new Course
                {
                    Name = request.Name
                };

                _context.Courses.Add(course);
                await _context.SaveChangesAsync();

                var tees = request.Tees.Select(t => new Tee
                {
                    Name = t.TeeName,
                    CourseRating = t.CourseRating,
                    Slope = t.Slope,
                    Course = course
                }).ToList();

                _context.Tees.AddRange(tees);
                await _context.SaveChangesAsync();
            }
        }
    }
}
