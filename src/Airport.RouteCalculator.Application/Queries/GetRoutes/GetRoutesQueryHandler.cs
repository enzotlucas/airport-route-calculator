namespace Airport.RouteCalculator.Application.Queries.GetRoutes
{
    public sealed class GetRoutesQueryHandler : IRequestHandler<GetRoutesQuery, IEnumerable<RouteViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetRoutesQueryHandler> _logger;

        public GetRoutesQueryHandler(IUnitOfWork unitOfWork,
                                  IMapper mapper,
                                  ILogger<GetRoutesQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<RouteViewModel>> Handle(GetRoutesQuery request, CancellationToken cancellationToken)
        {
            var routes = await _unitOfWork.Routes.GetAllAsync(request.Page, request.Rows);

            _logger.LogInformation("Routes was queried", routes);

            return _mapper.Map<IEnumerable<RouteViewModel>>(routes);
        }
    }
}
