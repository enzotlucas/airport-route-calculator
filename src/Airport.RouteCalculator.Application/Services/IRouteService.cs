namespace Airport.RouteCalculator.Application.Services
{
    public interface IRouteService
    {
        string GetBestRoute(GetBestRouteQuery request, IEnumerable<Route> routes);
    }
}
