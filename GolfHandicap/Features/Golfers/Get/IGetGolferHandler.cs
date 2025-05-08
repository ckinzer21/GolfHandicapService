namespace GolfHandicap.Features.Golfers.Get
{
    public interface IGetGolferHandler
    {
        public Task<IEnumerable<GetGolferResponse?>> GetAllGolfers();
        public Task<GetGolferResponse?> GetGolferById(int golferId);
    }
}
