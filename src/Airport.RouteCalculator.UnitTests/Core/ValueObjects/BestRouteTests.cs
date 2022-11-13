namespace Airport.RouteCalculator.UnitTests.Core.ValueObjects
{
    public class BestRouteTests
    {
        [Fact]
        public void Constructor_ShouldCreate()
        {
            //Arrange
            var route = new Route();
            var position = 0;

            //Act
            var bestRoute = new BestRoute(route, position);

            //Assert
            bestRoute.Route.Should().Be(route);
            bestRoute.Position.Should().Be(position);
        }
    }
}
