namespace Airport.RouteCalculator.UnitTests.Application.Commands
{
    [Collection(nameof(ApplicationFixtureCollection))]
    public class CreateRouteCommandTests
    {
        private readonly ApplicationFixture _fixture;

        public CreateRouteCommandTests(ApplicationFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Constructor_AnyInformations_ShouldCreateCommand()
        {
            //Arrange
            var routeViewModel = _fixture.RouteViewModel.GenerateValid();

            //Act
            var command = new CreateRouteCommand(routeViewModel);

            //Assert
            command.Should().NotBe(null);
        }

        [Fact]
        public async Task Handle_ValidInformation_ShouldReturnCreated()
        {
            //Arrange
            var route = _fixture.Route.GenerateValid();
            var routeViewModel = _fixture.RouteViewModel.GenerateValidFromEntity(route);
            var request = _fixture.CreateRouteCommand.GenerateCommandFromViewModel(routeViewModel);

            var sut = _fixture.CreateRouteCommand.GenerateValidHandler(request, route, routeViewModel);

            //Act
            var response = await sut.Handle(request, CancellationToken.None);

            //Assert
            response.Should().Be(routeViewModel);
            response.Should().NotBeNull();
        }

        [Fact]
        public void Handle_InvalidInformation_ShouldThrow()
        {
            //Arrange
            var route = _fixture.Route.GenerateInvalid();
            var routeViewModel = _fixture.RouteViewModel.GenerateValidFromEntity(route);
            var request = _fixture.CreateRouteCommand.GenerateCommandFromViewModel(routeViewModel);

            var sut = _fixture.CreateRouteCommand.GenerateInvalidHandler(exists: false,
                                                                         saveChanges: true);

            //Act
            var act = async () => { await sut.Handle(request, CancellationToken.None); };

            //Assert
            act.Should().ThrowExactlyAsync<InvalidRouteException>();
        }

        [Fact]
        public void Handle_ExistingRoute_ShouldThrow()
        {
            //Arrange
            var route = _fixture.Route.GenerateInvalid();
            var routeViewModel = _fixture.RouteViewModel.GenerateValidFromEntity(route);
            var request = _fixture.CreateRouteCommand.GenerateCommandFromViewModel(routeViewModel);

            var sut = _fixture.CreateRouteCommand.GenerateInvalidHandler(exists: true,
                                                                         saveChanges: true);

            //Act
            var act = async () => { await sut.Handle(request, CancellationToken.None); };

            //Assert
            act.Should().ThrowExactlyAsync<RouteExistsException>();
        }

        [Fact]
        public void Handle_ValidInformationServiceUnavailable_ShouldThrow()
        {
            //Arrange
            var route = _fixture.Route.GenerateValid();
            var routeViewModel = _fixture.RouteViewModel.GenerateValidFromEntity(route);
            var request = _fixture.CreateRouteCommand.GenerateCommandFromViewModel(routeViewModel);

            var sut = _fixture.CreateRouteCommand.GenerateInvalidHandler(exists: false,
                                                                         saveChanges: false);

            //Act
            var act = async () => { await sut.Handle(request, CancellationToken.None); };

            //Assert
            act.Should().ThrowExactlyAsync<InfrastructureException>();
        }
    }
}
