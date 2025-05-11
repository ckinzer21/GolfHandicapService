using GolfHandicap.Data;
using GolfHandicap.Entities;
using GolfHandicap.Features.Setup.Courses;
using GolfHandicap.Features.Setup.Flights;
using GolfHandicap.Features.Setup.Majors;

namespace GolfHandicap.Features.Setup
{
    public interface IPostSetupHandler
    {
        Task CreateMajors(IEnumerable<PostMajorRequest> requests);
        Task CreateFlight(IEnumerable<PostFlightRequest> request);
        Task CreateCourseAndTees(IEnumerable<CreateCourseRequest> requests);
        Task CreateWeight(double pct);
    }

    public class PostSetupHandler : IPostSetupHandler
    {
        private readonly DataContext _context;

        public PostSetupHandler(DataContext context)
        {
            _context = context;
        }

        public async Task CreateFlight(IEnumerable<PostFlightRequest> requests)
        {
            _context.Database.EnsureCreated();

            foreach (var request in requests)
            {
                var flight = new Flight { FlightId = request.flightId, Name = request.name };
                _context.Add(flight);
            }
            await _context.SaveChangesAsync();
        }

        public async Task CreateCourseAndTees(IEnumerable<CreateCourseRequest> requests)
        {
            foreach (var request in requests)
            {
                var course = new Course
                {
                    Name = request.Name
                };

                _context.Course.Add(course);
                await _context.SaveChangesAsync();

                var tees = request.Tees.Select(t => new Tee
                {
                    Name = t.TeeName,
                    CourseRating = t.CourseRating,
                    Slope = t.Slope,
                    Course = course
                }).ToList();

                _context.Tee.AddRange(tees);
                await _context.SaveChangesAsync();
            }
        }

        public async Task CreateMajors(IEnumerable<PostMajorRequest> requests)
        {
            foreach (var request in requests)
            {
                var major = new Major { MajorId = request.majorId, Name = request.name };
                _context.Add(major);
            }
            await _context.SaveChangesAsync();
        }

        public async Task CreateWeight(double pct)
        {
            var weight = new Weight { Value = pct };
            _context.Weight.Add(weight);
            await _context.SaveChangesAsync();
        }
    }
}
