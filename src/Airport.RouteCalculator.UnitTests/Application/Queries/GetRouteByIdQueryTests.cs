namespace Airport.RouteCalculator.UnitTests.Application.Queries
{
    [Collection(nameof(ApplicationFixtureCollection))]
    public class GetRouteByIdQueryTests
    {
        private readonly ApplicationFixture _fixture;

        public GetRouteByIdQueryTests(ApplicationFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Constuctor_AnyInformations_ShouldCreateQuery()
        {
            //Arrange
            var id = Guid.NewGuid();

            //Act
            var query = new GetRouteByIdQuery(id);

            //Assert
            query.Should().NotBe(null);
        }

        [Fact]
        public async Task Handle_ExistingRoute_ShouldReturnProduct()
        {
            //Arrange
            var route = _fixture.Route.GenerateValid();
            var request = _fixture.GetRouteByIdQuery.GenerateQueryByEntity(route);
            var routeViewModel = _fixture.RouteViewModel.GenerateValidFromEntity(route);

            var sut = _fixture.GetRouteByIdQuery.GenerateValidHandler(route, routeViewModel);

            //Act
            var response = await sut.Handle(request, CancellationToken.None);

            //Assert
            response.Should().Be(routeViewModel);
            response.Should().NotBeNull();
        }

        [Fact]
        public void Handle_ValidInformation_ShouldThrow()
        {
            //Arrange
            var request = _fixture.GetRouteByIdQuery.GenerateValidQuery(Guid.NewGuid());
            var sut = _fixture.GetRouteByIdQuery.GenerateInvalidHandler();

            //Act
            var act = async () => { await sut.Handle(request, CancellationToken.None); };

            //Assert
            act.Should().ThrowExactlyAsync<RouteNotFoundException>();
        }
    }
}
