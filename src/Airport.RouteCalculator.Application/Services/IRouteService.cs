namespace Airport.RouteCalculator.Application.Services
{
    public interface IRouteService
    {
        string GetBestRoute(GetBestCostRouteQuery request, IEnumerable<Route> routes);
    }
}
