namespace Airport.RouteCalculator.Core.Repositories
{
    public interface IRouteRepository
    {
        Task CreateAsync(Route route);
        Task DeleteAsync(Route route);
        Task<bool> ExistsAsync(Route route);
        Task<Route> GetByIdAsync(Guid id);
        Task UpdateAsync(Route route);
    }
}
