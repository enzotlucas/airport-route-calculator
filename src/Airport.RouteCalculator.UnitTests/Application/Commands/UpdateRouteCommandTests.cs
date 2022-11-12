using System;

namespace Airport.RouteCalculator.UnitTests.Application.Commands
{
    [Collection(nameof(ApplicationFixtureCollection))]
    public class UpdateRouteCommandTests
    {
        private readonly ApplicationFixture _fixture;

        public UpdateRouteCommandTests(ApplicationFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Constuctor_AnyInformations_ShouldCreateCommand()
        {
            //Arrange
            var routeViewModel = _fixture.RouteViewModel.GenerateValid();

            //Act
            var command = new UpdateRouteCommand(routeViewModel.Id, routeViewModel);

            //Assert
            command.Should().NotBe(null);
        }

        [Fact]
        public async Task Handle_ExistingRoute_ShouldUpdate()
        {
            //Arrange
            var route = _fixture.Route.GenerateValid();
            var rotueViewModel = _fixture.RouteViewModel.GenerateValid();
            var request = _fixture.UpdateRouteCommand.GenerateCommandFromViewModel(rotueViewModel);

            var sut = _fixture.UpdateRouteCommand.GenerateValidHandler(route);

            //Act
            var act = async () => await sut.Handle(request, CancellationToken.None);

            //Assert
            await act.Should().NotThrowAsync();
        }

        [Fact]
        public void Handle_ExistingRouteInvalidInformation_ShouldThrow()
        {
            //Arrange
            var route = _fixture.Route.GenerateValid();

            var routeViewModel = _fixture.RouteViewModel.GenerateValid();

            var firstInvalidFromRequest = new UpdateRouteCommand(routeViewModel.Id, _fixture.RouteViewModel.GenerateInvalid(InvalidRouteViewModelField.FROM, routeViewModel));
            var secondInvalidFromRequest = new UpdateRouteCommand(routeViewModel.Id, _fixture.RouteViewModel.GenerateInvalid(InvalidRouteViewModelField.FROM_EQUALS_TO, routeViewModel));
            var firstInvalidToRequest = new UpdateRouteCommand(routeViewModel.Id, _fixture.RouteViewModel.GenerateInvalid(InvalidRouteViewModelField.TO, routeViewModel));
            var secondInvalidToRequest = new UpdateRouteCommand(routeViewModel.Id, _fixture.RouteViewModel.GenerateInvalid(InvalidRouteViewModelField.TO_EQUALS_FROM, routeViewModel));
            var invalidValueRequest = new UpdateRouteCommand(routeViewModel.Id, _fixture.RouteViewModel.GenerateInvalid(InvalidRouteViewModelField.VALUE, routeViewModel));
           
            var sut = _fixture.UpdateRouteCommand.GenerateValidHandler(route);

            //Act
            var firstact = async () => await sut.Handle(firstInvalidFromRequest, CancellationToken.None);
            var secondAct = async () => await sut.Handle(secondInvalidFromRequest, CancellationToken.None);
            var thirdAct = async () => await sut.Handle(firstInvalidToRequest, CancellationToken.None);
            var fourthAct = async () => await sut.Handle(secondInvalidToRequest, CancellationToken.None);
            var fiftAct = async () => await sut.Handle(invalidValueRequest, CancellationToken.None);

            //Assert
            firstact.Should().ThrowExactlyAsync<InvalidFromException>();
            secondAct.Should().ThrowExactlyAsync<InvalidFromException>();
            thirdAct.Should().ThrowExactlyAsync<InvalidToException>();
            fourthAct.Should().ThrowExactlyAsync<InvalidToException>();
            fiftAct.Should().ThrowExactlyAsync<InvalidValueException>();
        }

        [Fact]
        public async Task Handle_UnexistingRoute_ShouldThrow()
        {
            //Arrange
            var route = _fixture.Route.GenerateInvalid();
            var routeViewModel = _fixture.RouteViewModel.GenerateValid();
            var request = _fixture.UpdateRouteCommand.GenerateCommandFromViewModel(routeViewModel);

            var sut = _fixture.UpdateRouteCommand.GenerateValidHandler(route);

            //Act
            var act = async () => await sut.Handle(request, CancellationToken.None);

            //Assert
            await act.Should().ThrowExactlyAsync<RouteNotFoundException>();
        }

        [Fact]
        public async Task Handle_ExistingRouteButServiceUnavailable_ShouldThrow()
        {
            //Arrange
            var route = _fixture.Route.GenerateValid();
            var routeViewModel = _fixture.RouteViewModel.GenerateValid();
            var request = _fixture.UpdateRouteCommand.GenerateCommandFromViewModel(routeViewModel);

            var sut = _fixture.UpdateRouteCommand.GenerateInvalidHandler(route);

            //Act
            var act = async () => await sut.Handle(request, CancellationToken.None);

            //Assert
            await act.Should().ThrowExactlyAsync<InfrastructureException>();
        }
    }
}
