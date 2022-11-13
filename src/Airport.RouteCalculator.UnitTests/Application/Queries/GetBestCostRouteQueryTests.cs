namespace Airport.RouteCalculator.UnitTests.Application.Queries
{
    [Collection(nameof(ApplicationFixtureCollection))]
    public class GetBestCostRouteQueryTests
    {
        private readonly ApplicationFixture _fixture;

        public GetBestCostRouteQueryTests(ApplicationFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Constructor_AnyInformations_ShouldReturnValidQuery()
        {
            //Arrange
            var from = "ABC";
            var to = "DEF";

            //Act
            var query = new GetBestCostRouteQuery(from, to);

            //Assert
            query.Should().NotBeNull();
        }

        [Fact]
        public async Task Handle_ExistingRoutes_ShouldReturnValidRoute()
        {
            //Arrange
            var routes = _fixture.Route.GenerateValidCollection(15);
            var expectedResponse = "ABC - DEF - GTD ao custo de $30";

            var request = new GetBestCostRouteQuery("ABC", "GTD");

            var sut = _fixture.GetBestRouteQuery.GenerateValidHandler(routes, expectedResponse);

            //Act
            var response = await sut.Handle(request, CancellationToken.None);

            //Assert
            response.Should().Be(expectedResponse);
            response.Should().NotBeNull();
        }

        [Fact]
        public void Handle_UnexistingRoutes_ShouldThrow()
        {
            //Arrange
            var routes = Enumerable.Empty<Route>();
            var request = new GetBestCostRouteQuery("ABC", "GTD");

            var sut = _fixture.GetBestRouteQuery.GenerateValidHandler(routes, string.Empty);

            //Act
            var act = () => sut.Handle(request, CancellationToken.None);

            //Assert
            act.Should().ThrowExactlyAsync<BusinessException>().WithMessage("Não existem rotas cadastradas");
        }
    }
}
