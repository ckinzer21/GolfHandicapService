using GolfHandicap.Data;
using GolfHandicap.Features.Controller;
using Microsoft.EntityFrameworkCore;

namespace GolfHandicap.Features.Matches.Post.GolfMatches.Preview
{
    public class PreviewGolfMatchHandler : IPreviewGolfMatchHandler
    {
        private readonly DataContext _dataContext;
        private readonly IPreviewGolfMatchValidator _validator;

        public PreviewGolfMatchHandler(DataContext dataContext, IPreviewGolfMatchValidator validator)
        {
            _dataContext = dataContext;
            _validator = validator;
        }

        // first the schedule will get created.  It should send over the entire year with the week and year number for when we play
        // We'll create the schedule and return it for a preview here
        // If good, then call the save
        public async Task<IEnumerable<GolfMatchResponse>> PreivewSchedule()
        {
            _dataContext.Database.EnsureCreated();
            var matchSchedule = _dataContext.MatchSchedules.Where(ms => ms.Year == DateTime.Today.Year);
            var golfers = await _dataContext.Golfers.ToListAsync();
            if (!matchSchedule.Any()) return new List<GolfMatchResponse>();
            
            

            return new List<GolfMatchResponse>();
        }
    }
}
