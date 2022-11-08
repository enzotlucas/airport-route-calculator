namespace Airport.RouteCalculator.UnitTests.Fixtures.API.Controllers
{
    public class RoutesControllerFixture
    {
        public RoutesController GenerateValid(RouteViewModel routeViewModel = null)
        {
            var mediator = Substitute.For<IMediator>();

            mediator.Send(Arg.Any<GetRouteByIdQuery>()).Returns(Task.FromResult(routeViewModel));

            mediator.Send(Arg.Any<CreateRouteCommand>()).Returns(Task.FromResult(routeViewModel));

            return new RoutesController(mediator);
        }

        public RoutesController GenerateValid(IEnumerable<RouteViewModel> routeViewModels)
        {
            var mediator = Substitute.For<IMediator>();

            mediator.Send(Arg.Any<GetRoutesQuery>()).Returns(Task.FromResult(routeViewModels));

            return new RoutesController(mediator);
        }

        public RoutesController GenerateInvalid(bool invalidRoute)
        {
            var mediator = Substitute.For<IMediator>();

            mediator.Send(Arg.Any<CreateRouteCommand>()).ThrowsAsync(new RouteExistsException());

            mediator.Send(Arg.Any<DeleteRouteCommand>()).ThrowsAsync(new RouteNotFoundException());

            mediator.Send(Arg.Any<GetRouteByIdQuery>()).ThrowsAsync(new RouteNotFoundException());

            mediator.Send(Arg.Any<UpdateRouteCommand>()).ThrowsAsync(new RouteNotFoundException());

            if (invalidRoute)
            {
                mediator.Send(Arg.Any<CreateRouteCommand>()).ThrowsAsync(new InvalidRouteException());

                mediator.Send(Arg.Any<UpdateRouteCommand>()).ThrowsAsync(new InvalidRouteException());
            }

            return new RoutesController(mediator);
        }
    }
}
