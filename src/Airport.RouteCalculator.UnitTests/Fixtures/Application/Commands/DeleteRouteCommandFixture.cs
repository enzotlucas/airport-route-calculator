namespace Airport.RouteCalculator.UnitTests.Fixtures.Application.Commands
{
    public class DeleteRouteCommandFixture
    {
        public DeleteRouteCommand GenerateCommandFromEntity(Route route)
        {
            return new DeleteRouteCommand(route.Id);
        }

        public DeleteRouteCommandHandler GenerateValidHandler(Route route)
        {
            var uow = Substitute.For<IUnitOfWork>();
            uow.Routes.GetByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult(route));
            uow.SaveChangesAsync().Returns(true);

            var logger = Substitute.For<ILogger<DeleteRouteCommandHandler>>();

            return new DeleteRouteCommandHandler(uow, logger);
        }

        public DeleteRouteCommandHandler GenerateInvalidHandler(Route route)
        {
            var uow = Substitute.For<IUnitOfWork>();
            uow.Routes.GetByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult(route));
            uow.SaveChangesAsync().Returns(false);

            var logger = Substitute.For<ILogger<DeleteRouteCommandHandler>>();

            return new DeleteRouteCommandHandler(uow, logger);
        }
    }
}
