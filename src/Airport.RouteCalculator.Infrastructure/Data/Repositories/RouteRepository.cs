using System.Data.SqlClient;

namespace Airport.RouteCalculator.Infrastructure.Data.Repositories
{
    public sealed class RouteRepository : IRouteRepository
    {
        private readonly SqlServerContext _context;

        public RouteRepository(SqlServerContext context)
        {
            _context = context;
        }

        public async Task<Route> CreateAsync(Route route)
        {
            await _context.Routes.AddAsync(route);

            return route;
        }

        public Task DeleteAsync(Route route)
        {
            _context.Routes.Remove(route);

            return Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(Route route)
        {
            return await _context.Routes
                                  .Where(r => r.From.ToUpper() == route.From.ToUpper() &&
                                              r.To.ToUpper() == route.To.ToUpper())
                                  .AnyAsync();
        }

        public async Task<IEnumerable<Route>> GetAllAsync(int? page = null, int? rows = null)
        {
            if (page is null || page.Value < 1 || rows is null || rows.Value < 1)
            {
                return await _context.Routes.ToListAsync();
            }

            return await GetPagedRoutes(page.Value, rows.Value);
        }

        private async Task<IEnumerable<Route>> GetPagedRoutes(int page, int rows)
        {
            var query = QueriesExtensions.PagedRoutes;

            var dbConnection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString);

            return await dbConnection.QueryAsync<Route>(
               query,
               new
               {
                   page,
                   rows
               }
            );
        }

        public async Task<Route> GetByIdAsync(Guid id)
        {
            var route = await _context.Routes.FirstOrDefaultAsync(r => r.Id == id);

            return route ?? new Route();
        }

        public Task UpdateAsync(Route route)
        {
            _context.Routes.Update(route);

            return Task.CompletedTask;
        }
    }
}
