namespace Airport.RouteCalculator.UnitTests.Core.ValueObjects
{
    [Collection(nameof(CoreFixtureCollection))]
    public class BestRoutesOrderTests
    {
        private readonly CoreFixture _fixture;

        public BestRoutesOrderTests(CoreFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Constructor_NoParameters_ShouldCreateWithRouteList()
        {
            //Arrange & Act
            var bestRoutesOrder = new BestRoutesOrder();

            //Assert
            bestRoutesOrder.BestRoutes.Should().NotBeNull();
            bestRoutesOrder.BestRoutes.Should().BeEmpty();
        }

        [Fact]
        public void Constructor_ValidRoute_ShouldCreateWithRouteListWithOneRoute()
        {
            //Arrange
            var route = _fixture.Route.GenerateValid();

            //Act
            var bestRoutesOrder = new BestRoutesOrder(route);

            //Assert
            bestRoutesOrder.BestRoutes.Should().NotBeNullOrEmpty();
            bestRoutesOrder.BestRoutes.Select(b => b.Route).Any(r => r == route).Should().BeTrue();
        }
    }
}
