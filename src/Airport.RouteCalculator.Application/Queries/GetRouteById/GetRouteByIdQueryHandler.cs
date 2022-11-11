namespace Airport.RouteCalculator.Application.Queries.GetRouteById
{
    public sealed class GetRouteByIdQueryHandler : IRequestHandler<GetRouteByIdQuery, RouteViewModel>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<GetRouteByIdQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetRouteByIdQueryHandler(IUnitOfWork uow,
                                        ILogger<GetRouteByIdQueryHandler> logger,
                                        IMapper mapper)
        {
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<RouteViewModel> Handle(GetRouteByIdQuery request, CancellationToken cancellationToken)
        {
            var route = await _uow.Routes.GetByIdAsync(request.Id);

            if (!route.IsValid)
            {
                throw new RouteNotFoundException();
            }

            _logger.LogInformation("Route was queried", route);

            return _mapper.Map<RouteViewModel>(route);
        }
    }
}
