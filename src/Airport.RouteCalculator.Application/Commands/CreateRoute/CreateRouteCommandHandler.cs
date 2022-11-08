namespace Airport.RouteCalculator.Application.Commands.CreateRoute
{
    public class CreateRouteCommandHandler : IRequestHandler<CreateRouteCommand, RouteViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRouteCommandHandler(IUnitOfWork unitOfWork, 
                                         IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RouteViewModel> Handle(CreateRouteCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
