namespace Airport.RouteCalculator.Application.Commands.UpdateRoute
{
    public class UpdateRouteCommandHandler : IRequestHandler<UpdateRouteCommand>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<UpdateRouteCommandHandler> _logger;

        public UpdateRouteCommandHandler(IUnitOfWork uow,
                                         ILogger<UpdateRouteCommandHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateRouteCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Route update attempt", request.Id);

            var route = await _uow.Routes.GetByIdAsync(request.Id);

            if (!route.IsValid)
            {
                throw new RouteNotFoundException();
            }

            route.Update(request.From,
                         request.To,
                         request.Value);

            await _uow.Routes.UpdateAsync(route);

            if (!await _uow.SaveChangesAsync())
            {
                throw new InfrastructureException("Não foi possível atualizar a rota.");
            }

            _logger.LogInformation("Route updated", route);

            return Unit.Value;
        }
    }
}
