namespace Airport.RouteCalculator.Application.Services
{
    public interface IRouteService
    {
        string GetBestCostRoute(GetBestCostRouteQuery request, IEnumerable<Route> routes);
    }
}
