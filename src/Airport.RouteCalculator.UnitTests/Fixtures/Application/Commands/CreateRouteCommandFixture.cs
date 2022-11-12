namespace Airport.RouteCalculator.UnitTests.Fixtures.Application.Commands
{
    public class CreateRouteCommandFixture
    {
        internal CreateRouteCommand GenerateCommandFromViewModel(RouteViewModel productViewModel)
        {
            return new(productViewModel);
        }

        internal CreateRouteCommandHandler GenerateInvalidHandler(bool exists, bool saveChanges)
        {
            var uow = Substitute.For<IUnitOfWork>();
            uow.Routes.ExistsAsync(Arg.Any<Route>()).Returns(Task.FromResult(exists));
            uow.SaveChangesAsync().Returns(Task.FromResult(saveChanges));

            var logger = Substitute.For<ILogger<CreateRouteCommandHandler>>();

            var mapper = Substitute.For<IMapper>();

            return new CreateRouteCommandHandler(uow, logger, mapper);
        }

        internal CreateRouteCommandHandler GenerateValidHandler(CreateRouteCommand request, Route route, RouteViewModel routeViewModel)
        {
            var uow = Substitute.For<IUnitOfWork>();
            uow.Routes.ExistsAsync(route).Returns(Task.FromResult(false));
            uow.Routes.CreateAsync(route).Returns(Task.FromResult(route));
            uow.SaveChangesAsync().Returns(Task.FromResult(true));

            var logger = Substitute.For<ILogger<CreateRouteCommandHandler>>();

            var mapper = Substitute.For<IMapper>();
            mapper.Map<Route>(request).Returns(route);
            mapper.Map<RouteViewModel>(route).Returns(routeViewModel);

            return new CreateRouteCommandHandler(uow, logger, mapper);
        }
    }
}
