namespace Airport.RouteCalculator.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SqlServerContext _context;
        public IRouteRepository Routes { get; }

        public UnitOfWork(SqlServerContext context, 
                          IRouteRepository routeRepository)
        {
            _context = context;
            Routes = routeRepository;
        }

        public async Task<bool> SaveChangesAsync()
        {
            foreach (var entry in _context.ChangeTracker.Entries()
                                                        .Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null && 
                                                                        entry.Entity.GetType().GetProperty("UpdatedAt") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                    entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreatedAt").IsModified = false;
                    entry.Property("UpdatedAt").CurrentValue = DateTime.Now;
                }
            }                

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
