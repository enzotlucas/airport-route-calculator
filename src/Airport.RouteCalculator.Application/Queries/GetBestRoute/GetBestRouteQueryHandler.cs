namespace Airport.RouteCalculator.Application.Queries.GetBestRoute
{
    public class GetBestRouteQueryHandler : IRequestHandler<GetBestRouteQuery, string>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<GetBestRouteQueryHandler> _logger;

        public GetBestRouteQueryHandler(IUnitOfWork uow, 
                                        ILogger<GetBestRouteQueryHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public Task<string> Handle(GetBestRouteQuery request, CancellationToken cancellationToken)
        {
            //var routes = _uow.Routes.GetAllAsync();

            return Task.FromResult(string.Empty);
        }
    }
}
