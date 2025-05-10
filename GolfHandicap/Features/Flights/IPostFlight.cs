namespace GolfHandicap.Features.Flights
{
    public interface IPostFlight
    {
        Task CreateFlight(IEnumerable<PostFlightRequest> request);
    }
}
