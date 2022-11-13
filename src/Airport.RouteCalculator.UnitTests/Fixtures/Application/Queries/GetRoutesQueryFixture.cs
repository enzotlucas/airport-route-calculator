namespace Airport.RouteCalculator.UnitTests.Fixtures.Application.Queries
{
    public class GetRoutesQueryFixture
    {
        public GetRoutesQueryHandler GenerateValidHandler(IEnumerable<Route> routes,
                                                            IEnumerable<RouteViewModel> routeViewModels)
        {
            var uow = Substitute.For<IUnitOfWork>();
            uow.Routes.GetAllAsync(Arg.Any<int>(), Arg.Any<int>()).Returns(routes);

            var mapper = Substitute.For<IMapper>();
            mapper.Map<IEnumerable<RouteViewModel>>(Arg.Any<IEnumerable<Route>>()).Returns(routeViewModels);

            var logger = Substitute.For<ILogger<GetRoutesQueryHandler>>();

            return new GetRoutesQueryHandler(uow, mapper, logger);
        }
    }
}
