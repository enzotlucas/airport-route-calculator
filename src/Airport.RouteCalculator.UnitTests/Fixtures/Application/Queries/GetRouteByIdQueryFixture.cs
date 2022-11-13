namespace Airport.RouteCalculator.UnitTests.Fixtures.Application.Queries
{
    public class GetRouteByIdQueryFixture
    {
        public GetRouteByIdQueryHandler GenerateValidHandler(Route route, RouteViewModel routeViewModel)
        {
            var uow = Substitute.For<IUnitOfWork>();
            uow.Routes.GetByIdAsync(Arg.Any<Guid>()).Returns(route);

            var logger = Substitute.For<ILogger<GetRouteByIdQueryHandler>>();

            var mapper = Substitute.For<IMapper>();
            mapper.Map<RouteViewModel>(Arg.Any<Route>()).Returns(routeViewModel);

            return new GetRouteByIdQueryHandler(uow, logger, mapper);
        }

        public GetRouteByIdQueryHandler GenerateInvalidHandler()
        {
            var uow = Substitute.For<IUnitOfWork>();
            uow.Routes.GetByIdAsync(Arg.Any<Guid>()).Returns(new Route());

            var logger = Substitute.For<ILogger<GetRouteByIdQueryHandler>>();

            var mapper = Substitute.For<IMapper>();

            return new GetRouteByIdQueryHandler(uow, logger, mapper);
        }

        public GetRouteByIdQuery GenerateValidQuery(Guid id)
        {
            return new GetRouteByIdQuery(id);
        }

        public GetRouteByIdQuery GenerateQueryByEntity(Route route)
        {
            return new GetRouteByIdQuery(route.Id);
        }
    }
}
