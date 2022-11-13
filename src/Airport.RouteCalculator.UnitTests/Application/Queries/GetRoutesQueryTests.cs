using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.RouteCalculator.UnitTests.Application.Queries
{
    [Collection(nameof(ApplicationFixtureCollection))]
    public class GetRoutesQueryTests
    {
        private readonly ApplicationFixture _fixture;

        public GetRoutesQueryTests(ApplicationFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Constructor_AnyInformations_ShouldReturnValidQuery()
        {
            //Arrange
            var page = 1;
            var rows = 10;

            //Act
            var query = new GetRoutesQuery(page, rows);

            //Assert
            query.Should().NotBeNull();
        }

        [Fact]
        public async Task Handle_ExistingRoutes_ShouldReturnProductList()
        {
            //Arrange
            var routes = _fixture.Route.GenerateValidCollection(5);
            var routeViewModels = _fixture.RouteViewModel.GenerateValidCollectionFromEntity(routes);
            var request = new GetRoutesQuery(page: 1, rows: 10);

            var sut = _fixture.GetRoutesQuery.GenerateValidHandler(routes, routeViewModels);

            //Act
            var response = await sut.Handle(request, CancellationToken.None);

            //Assert
            response.Should().NotBeNull();
            response.Should().NotBeEmpty();
            response.Equals(routeViewModels).Should().BeTrue();
            response.Equals(routes).Should().BeFalse();
        }

        [Fact]
        public async Task Handle_UnexistingRoutes_ShouldReturnEmptyProductList()
        {
            //Arrange
            var routes = new List<Route>();
            var routeViewModels = new List<RouteViewModel>();
            var request = new GetRoutesQuery(page: 1, rows: 10);

            var sut = _fixture.GetRoutesQuery.GenerateValidHandler(routes, routeViewModels);

            //Act
            var response = await sut.Handle(request, CancellationToken.None);

            //Assert
            response.Should().NotBeNull();
            response.Should().BeEmpty();
            response.Equals(routeViewModels).Should().BeTrue();
            response.Equals(routes).Should().BeFalse();
        }

        [Fact]
        public async Task Handle_ExistingRoutesInvalidPagesAndRows_ShouldReturnAllRoutes()
        {
            //Arrange
            var firstRequest = new GetRoutesQuery(page: null, rows: 10);
            var secondRequest = new GetRoutesQuery(page: 1, rows: null);
            var thirdRequest = new GetRoutesQuery(page: null, rows: null);
            var fourthRequest = new GetRoutesQuery(page: 0, rows: 10);
            var fiftRequest = new GetRoutesQuery(page: 1, rows: 0);

            var routes = _fixture.Route.GenerateValidCollection(15);
            var routeViewModels = _fixture.RouteViewModel.GenerateValidCollectionFromEntity(routes);

            var sut = _fixture.GetRoutesQuery.GenerateValidHandler(routes, routeViewModels);

            //Act
            var firstResponse = await sut.Handle(firstRequest, CancellationToken.None);
            var secondResponse = await sut.Handle(secondRequest, CancellationToken.None);
            var thirdResponse = await sut.Handle(thirdRequest, CancellationToken.None);
            var fourthResponse = await sut.Handle(fourthRequest, CancellationToken.None);
            var fiftResponse = await sut.Handle(fiftRequest, CancellationToken.None);

            //Assert
            firstResponse.Should().NotBeNull();
            firstResponse.Should().NotBeEmpty();
            firstResponse.Equals(routeViewModels).Should().BeTrue();
            firstResponse.Equals(routes).Should().BeFalse();

            secondResponse.Should().NotBeNull();
            secondResponse.Should().NotBeEmpty();
            secondResponse.Equals(routeViewModels).Should().BeTrue();
            secondResponse.Equals(routes).Should().BeFalse();

            thirdResponse.Should().NotBeNull();
            thirdResponse.Should().NotBeEmpty();
            thirdResponse.Equals(routeViewModels).Should().BeTrue();
            thirdResponse.Equals(routes).Should().BeFalse();

            fourthResponse.Should().NotBeNull();
            fourthResponse.Should().NotBeEmpty();
            fourthResponse.Equals(routeViewModels).Should().BeTrue();
            fourthResponse.Equals(routes).Should().BeFalse();

            fiftResponse.Should().NotBeNull();
            fiftResponse.Should().NotBeEmpty();
            fiftResponse.Equals(routeViewModels).Should().BeTrue();
            fiftResponse.Equals(routes).Should().BeFalse();
        }
    }
}
