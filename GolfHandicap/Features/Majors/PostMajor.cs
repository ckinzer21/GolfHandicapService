using GolfHandicap.Data;
using GolfHandicap.Entities;

namespace GolfHandicap.Features.Majors
{
    public class PostMajor : IPostMajor
    {
        private readonly DataContext _context;

        public PostMajor(DataContext context)
        {
            _context = context;
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
    }
}
