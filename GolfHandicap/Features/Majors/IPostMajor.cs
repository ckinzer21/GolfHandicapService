namespace GolfHandicap.Features.Majors
{
    public interface IPostMajor
    {
        Task CreateMajors(IEnumerable<PostMajorRequest> requests);
    }
}
