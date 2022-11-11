namespace Airport.RouteCalculator.UnitTests.Application.Services
{
    public class RouteServiceTests
    {
        [Fact]
        public void GetBestCostRoute_ExistingFromAndToRoute_ShouldReturnBestCostOne()
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

            var request = new GetBestCostRouteQuery("GRU", "CDG");

            var sut = new RouteService();

            //Act
            var response = sut.GetBestCostRoute(request, routes);

            //Assert
            response.Should().NotBeNull();
            response.Should().Be("GRU - BRC - SCL - ORL - CDG ao custo de $40");
        }

        [Fact]
        public void GetBestCostRoute_ExistingRoutesButNoFromAndToRoute_ShouldThrow()
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

            var request = new GetBestCostRouteQuery("SCL", "BRC");

            var sut = new RouteService();

            //Act
            var act = () => sut.GetBestCostRoute(request, routes);

            //Assert
            act.Should().ThrowExactly<BusinessException>().WithMessage("Não existe rota que vá da origem ao destino informado.");
        }

        [Fact]
        public void GetBestCostRoute_ExistingRoutesButNoFromRoute_ShouldThrow()
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

            var request = new GetBestCostRouteQuery("ABC", "BRC");

            var sut = new RouteService();

            //Act
            var act = () => sut.GetBestCostRoute(request, routes);

            //Assert
            act.Should().ThrowExactly<BusinessException>().WithMessage("Não existem rotas cadastradas que partem dessa origem");
        }

        [Fact]
        public void GetBestCostRoute_ExistingRoutesButNoToRoute_ShouldThrow()
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

            var request = new GetBestCostRouteQuery("GRU", "ABC");

            var sut = new RouteService();

            //Act
            var act = () => sut.GetBestCostRoute(request, routes);

            //Assert
            act.Should().ThrowExactly<BusinessException>().WithMessage("Não existem rotas cadastradas para esse destino");
        }
    }
}
