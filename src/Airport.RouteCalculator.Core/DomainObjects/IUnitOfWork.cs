namespace Airport.RouteCalculator.Core.DomainObjects
{
    public interface IUnitOfWork
    {
        IRouteRepository RouteRepository { get; }

        Task<bool> SaveChangesAsync();
    }
}
