namespace Airport.RouteCalculator.Application.Queries.GetBestCostRoute
{
    public sealed class GetBestCostRouteQueryHandler : IRequestHandler<GetBestCostRouteQuery, string>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRouteService _service;
        private readonly ILogger<GetBestCostRouteQueryHandler> _logger;

        public GetBestCostRouteQueryHandler(IUnitOfWork uow,
                                            IRouteService service,
                                            ILogger<GetBestCostRouteQueryHandler> logger)
        {
            _uow = uow;
            _service = service;
            _logger = logger;
        }

        public async Task<string> Handle(GetBestCostRouteQuery request, CancellationToken cancellationToken)
        {
            var routes = await _uow.Routes.GetAllAsync();

            if(!routes.Any())
            {
                throw new BusinessException("Não existem rotas cadastradas");
            }

            var bestRoute = _service.GetBestCostRoute(request, routes);

            _logger.LogInformation($"Request to best route, from {request.From} to {request.To}.", bestRoute);

            return bestRoute;
        }
    }
}
