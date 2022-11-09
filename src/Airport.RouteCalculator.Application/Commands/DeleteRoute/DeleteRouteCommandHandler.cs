namespace Airport.RouteCalculator.Application.Commands.DeleteRoute
{
    public class DeleteRouteCommandHandler : IRequestHandler<DeleteRouteCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DeleteRouteCommandHandler> _logger;

        public DeleteRouteCommandHandler(IUnitOfWork uow,
                                         ILogger<DeleteRouteCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteRouteCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting route", request.Id);

            var route = await _uow.Routes.GetByIdAsync(request.Id);

            if (!route.IsValid)
            {
                throw new RouteNotFoundException();
            }

            await _uow.Routes.DeleteAsync(route);

            if (!await _uow.SaveChangesAsync())
            {
                throw new InfrastructureException("Ocorreu um erro ao excluir a rota.");
            }

            _logger.LogInformation("Route deleted", route);

            return Unit.Value;
        }
    }
}
