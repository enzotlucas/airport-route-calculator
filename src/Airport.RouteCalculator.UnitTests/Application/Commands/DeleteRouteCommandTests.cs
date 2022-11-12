namespace Airport.RouteCalculator.UnitTests.Application.Commands
{
    [Collection(nameof(ApplicationFixtureCollection))]
    public class DeleteRouteCommandTests
    {
        private readonly ApplicationFixture _fixture;

        public DeleteRouteCommandTests(ApplicationFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Constuctor_AnyInformations_ShouldCreateCommand()
        {
            //Arrange
            var id = Guid.NewGuid();

            //Act
            var command = new DeleteRouteCommand(id);

            //Assert
            command.Should().NotBe(null);
        }

        [Fact]
        public async Task Handle_ExistingRoute_ShouldDelete()
        {
            //Arrange
            var route = _fixture.Route.GenerateValid();
            var request = _fixture.DeleteRouteCommand.GenerateCommandFromEntity(route);

            var sut = _fixture.DeleteRouteCommand.GenerateValidHandler(route);

            //Act
            var act = async () => await sut.Handle(request, CancellationToken.None);

            //Assert
            await act.Should().NotThrowAsync();
        }

        [Fact]
        public async Task Handle_UnexistingRoute_ShouldThrow()
        {
            //Arrange
            var route = _fixture.Route.GenerateInvalid();
            var request = _fixture.DeleteRouteCommand.GenerateCommandFromEntity(route);

            var sut = _fixture.DeleteRouteCommand.GenerateValidHandler(route);

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
            var request = _fixture.DeleteRouteCommand.GenerateCommandFromEntity(route);

            var sut = _fixture.DeleteRouteCommand.GenerateInvalidHandler(route);

            //Act
            var act = async () => await sut.Handle(request, CancellationToken.None);

            //Assert
            await act.Should().ThrowExactlyAsync<InfrastructureException>();
        }
    }
}
