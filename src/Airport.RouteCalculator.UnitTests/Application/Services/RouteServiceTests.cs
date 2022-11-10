namespace Airport.RouteCalculator.UnitTests.Application.Services
{
    public class RouteServiceTests
    {
        [Fact]
        public void GetBestRoute_ExistingFromAndToRoute_ShouldReturnBestCostOne()
        {
            //Arrange
            var validator = new RouteValidator();
            var routes = new List<Route>
            {
                new Route("GRU","BRC",10,validator),
                new Route("BRC","SCL",5,validator),
                new Route("GRU","CDG",75,validator),
                new Route("GRU","SCL",20,validator),
                new Route("GRU","ORL",56,validator),
                new Route("ORL","CDG",5,validator),
                new Route("SCL","ORL",20,validator)
            };

            var request = new GetBestRouteQuery("GRU", "CDG");

            var sut = new RouteService();

            //Act
            var response = sut.GetBestRoute(request, routes);

            //Assert
            response.Should().NotBeNull();
            response.Should().Be("GRU - BRC - SCL - ORL - CDG ao custo de $40");
        }
    }
}
