namespace GolfHandicap.Features.Courses
{
    public interface ICreateCourseHandler
    {
        Task Create(IEnumerable<CreateCourseRequest> requests);
    }
}
