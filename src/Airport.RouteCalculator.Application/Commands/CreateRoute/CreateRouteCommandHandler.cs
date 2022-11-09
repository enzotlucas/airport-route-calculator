namespace Airport.RouteCalculator.Application.Commands.CreateRoute
{
    public class CreateRouteCommandHandler : IRequestHandler<CreateRouteCommand, RouteViewModel>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<CreateRouteCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateRouteCommandHandler(IUnitOfWork uow,
                                         ILogger<CreateRouteCommandHandler> logger,
                                         IMapper mapper)
        {
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<RouteViewModel> Handle(CreateRouteCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Route creation atempt", request);

            var route = _mapper.Map<Route>(request);

            if (await _uow.Routes.ExistsAsync(route))
            {
                throw new RouteExistsException();
            }

            await _uow.Routes.CreateAsync(route);

            if (!await _uow.SaveChangesAsync())
            {
                throw new InfrastructureException("Ocorreu um erro ao criar a rota.");
            }

            _logger.LogInformation($"Route created, product id: {route.Id}", route);

            return _mapper.Map<RouteViewModel>(route);
        }
    }
}
