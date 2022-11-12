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
    }
}
