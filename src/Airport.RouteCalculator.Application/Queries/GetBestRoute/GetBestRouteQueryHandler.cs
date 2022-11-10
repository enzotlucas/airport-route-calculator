using Airport.RouteCalculator.Application.Services;

namespace Airport.RouteCalculator.Application.Queries.GetBestRoute
{
    public class GetBestRouteQueryHandler : IRequestHandler<GetBestRouteQuery, string>
    {
        private readonly IUnitOfWork _uow;
        private readonly IRouteService _service;
        private readonly ILogger<GetBestRouteQueryHandler> _logger;

        public GetBestRouteQueryHandler(IUnitOfWork uow,
                                        IRouteService service,
                                        ILogger<GetBestRouteQueryHandler> logger)
        {
            _uow = uow;
            _service = service;
            _logger = logger;
        }

        public async Task<string> Handle(GetBestRouteQuery request, CancellationToken cancellationToken)
        {
            var routes = await _uow.Routes.GetAllAsync();

            if(!routes.Any())
            {
                throw new BusinessException("Não existem rotas cadastradas");
            }

            var bestRoute = _service.GetBestRoute(request, routes);

            _logger.LogInformation($"Request to best route, from {request.From} to {request.To}.", bestRoute);

            return bestRoute;
        }
    }
}
