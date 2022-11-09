namespace Airport.RouteCalculator.Core.DomainObjects
{
    public interface IUnitOfWork
    {
        IRouteRepository Routes { get; }

        Task<bool> SaveChangesAsync();
    }
}
