namespace Airport.RouteCalculator.UnitTests.API.Controllers
{
    [Collection(nameof(ApiFixtureCollection))]
    public class RoutesControllerTests
    {
        private readonly ApiFixture _fixture;

        public RoutesControllerTests(ApiFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetById_ExistingRoute_ShouldReturnRouteViewModel()
        {
            //Arrange
            var routeViewModel = _fixture.RouteViewModel.GenerateValid();

            var sut = _fixture.RoutesController.GenerateValid(routeViewModel);

            //Act
            var response = await sut.GetById(routeViewModel.Id) as ObjectResult;

            //Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Should().BeAssignableTo<OkObjectResult>();
            response.Value.Should().BeAssignableTo<RouteViewModel>();
            response.Value.Should().Be(routeViewModel);
        }

        [Fact]
        public void GetById_UnexistingRoute_ShouldThrow()
        {
            //Arrange
            var sut = _fixture.RoutesController.GenerateInvalid(false);

            //Act
            var act = async () => { await sut.GetById(Guid.NewGuid()); };

            //Assert
            act.Should().ThrowExactlyAsync<RouteNotFoundException>();
        }

        [Fact]
        public async Task Get_ExistingRoute_ShouldReturnRouteViewModelColection()
        {
            //Arrange
            var routeViewModels = _fixture.RouteViewModel.GenerateValidCollection(5);

            var sut = _fixture.RoutesController.GenerateValid(routeViewModels);

            //Act
            var response = await sut.Get(page: 1, pageCount: 10) as ObjectResult;

            //Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Should().BeAssignableTo<OkObjectResult>();
            response.Value.Should().BeAssignableTo<IEnumerable<RouteViewModel>>();
            response.Value.Should().Be(routeViewModels);
        }

        [Fact]
        public async Task Get_UnexistingRoute_ShouldReturnEmptyCollection()
        {
            //Arrange
            var routeViewModels = new List<RouteViewModel>();

            var sut = _fixture.RoutesController.GenerateValid(routeViewModels);

            //Act
            var response = await sut.Get(page: 1, pageCount: 10) as ObjectResult;

            //Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Should().BeAssignableTo<OkObjectResult>();
            response.Value.Should().BeAssignableTo<IEnumerable<RouteViewModel>>();
            response.Value.Should().Be(routeViewModels);
        }

        [Fact]
        public async Task Get_ExistingRoutesInvalidPagesAndRows_ShouldReturnAllRoutes()
        {
            //Arrange
            var routeViewModels = _fixture.RouteViewModel.GenerateValidCollection(5);
            var firstRequest = new GetRoutesQuery(page: null, rows: 10);
            var secondRequest = new GetRoutesQuery(page: 1, rows: null);
            var thirdRequest = new GetRoutesQuery(page: null, rows: null);
            var fourthRequest = new GetRoutesQuery(page: 0, rows: 10);
            var fiftRequest = new GetRoutesQuery(page: 1, rows: 0);

            var sut = _fixture.RoutesController.GenerateValid(routeViewModels);

            //Act
            var firstResponse = await sut.Get(firstRequest.Page, firstRequest.Rows) as ObjectResult;
            var secondResponse = await sut.Get(secondRequest.Page, secondRequest.Rows) as ObjectResult;
            var thirdResponse = await sut.Get(thirdRequest.Page, thirdRequest.Rows) as ObjectResult;
            var fourthResponse = await sut.Get(fourthRequest.Page, fourthRequest.Rows) as ObjectResult;
            var fiftResponse = await sut.Get(fiftRequest.Page, fiftRequest.Rows) as ObjectResult;

            //Assert
            firstResponse.Should().NotBeNull();
            firstResponse.StatusCode.Should().Be(StatusCodes.Status200OK);
            firstResponse.Should().BeAssignableTo<OkObjectResult>();
            firstResponse.Value.Should().BeAssignableTo<IEnumerable<RouteViewModel>>();
            firstResponse.Value.Should().Be(routeViewModels);

            secondResponse.Should().NotBeNull();
            secondResponse.StatusCode.Should().Be(StatusCodes.Status200OK);
            secondResponse.Should().BeAssignableTo<OkObjectResult>();
            secondResponse.Value.Should().BeAssignableTo<IEnumerable<RouteViewModel>>();
            secondResponse.Value.Should().Be(routeViewModels);

            thirdResponse.Should().NotBeNull();
            thirdResponse.StatusCode.Should().Be(StatusCodes.Status200OK);
            thirdResponse.Should().BeAssignableTo<OkObjectResult>();
            thirdResponse.Value.Should().BeAssignableTo<IEnumerable<RouteViewModel>>();
            thirdResponse.Value.Should().Be(routeViewModels);

            fourthResponse.Should().NotBeNull();
            fourthResponse.StatusCode.Should().Be(StatusCodes.Status200OK);
            fourthResponse.Should().BeAssignableTo<OkObjectResult>();
            fourthResponse.Value.Should().BeAssignableTo<IEnumerable<RouteViewModel>>();
            fourthResponse.Value.Should().Be(routeViewModels);

            fiftResponse.Should().NotBeNull();
            fiftResponse.StatusCode.Should().Be(StatusCodes.Status200OK);
            fiftResponse.Should().BeAssignableTo<OkObjectResult>();
            fiftResponse.Value.Should().BeAssignableTo<IEnumerable<RouteViewModel>>();
            fiftResponse.Value.Should().Be(routeViewModels);
        }

        [Fact]
        public async Task Post_ValidInformations_ShouldReturnCreated()
        {
            //Arrange
            var routeViewModel = _fixture.RouteViewModel.GenerateValid();

            var sut = _fixture.RoutesController.GenerateValid(routeViewModel);

            //Act
            var response = await sut.Post(routeViewModel) as ObjectResult;

            //Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(StatusCodes.Status201Created);
            response.Value.Should().BeAssignableTo<RouteViewModel>();
            response.Value.Should().Be(routeViewModel);
        }

        [Fact]
        public void Post_InvalidInformations_ShouldThrow()
        {
            //Arrange
            var routeViewModel = _fixture.RouteViewModel.GenerateInvalid();

            var sut = _fixture.RoutesController.GenerateInvalid(true);

            //Act
            var act = async () => { await sut.Post(routeViewModel); };

            //Assert
            act.Should().ThrowExactlyAsync<InvalidRouteException>();
        }

        [Fact]
        public void Post_ExistingRoute_ShouldThrow()
        {
            //Arrange
            var routeViewModel = _fixture.RouteViewModel.GenerateValid();

            var sut = _fixture.RoutesController.GenerateInvalid(false);

            //Act
            var act = async () => { await sut.Post(routeViewModel); };

            //Assert
            act.Should().ThrowExactlyAsync<RouteExistsException>();
        }

        [Fact]
        public async Task Delete_ExistingRoute_ShouldReturnNoContent()
        {
            //Arrange
            var sut = _fixture.RoutesController.GenerateValid();

            //Act
            var response = await sut.Delete(Guid.NewGuid()) as NoContentResult;

            //Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(StatusCodes.Status204NoContent);
            response.Should().BeAssignableTo<NoContentResult>();
        }

        [Fact]
        public void Delete_UnexistingRoute_ShouldThrow()
        {
            //Arrange
            var sut = _fixture.RoutesController.GenerateInvalid(false);

            //Act
            var act = async () => { await sut.Delete(Guid.NewGuid()); };

            //Assert
            act.Should().ThrowExactlyAsync<RouteNotFoundException>();
        }

        [Fact]
        public async Task Put_ExistingRoute_ShouldReturnNoContent()
        {
            //Arrange
            var sut = _fixture.RoutesController.GenerateValid();

            //Act
            var response = await sut.Put(Guid.NewGuid(), _fixture.RouteViewModel.GenerateValid()) as NoContentResult;

            //Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(StatusCodes.Status204NoContent);
            response.Should().BeAssignableTo<NoContentResult>();
        }

        [Fact]
        public void Put_UnexistingRoute_ShouldThrow()
        {
            //Arrange
            var sut = _fixture.RoutesController.GenerateInvalid(false);

            //Act
            var act = async () => { await sut.Put(Guid.NewGuid(), _fixture.RouteViewModel.GenerateValid()); };

            //Assert
            act.Should().ThrowExactlyAsync<RouteNotFoundException>();
        }

        [Fact]
        public void Put_ExistingRouteInvalidInformation_ShouldThrow()
        {
            //Arrange
            var sut = _fixture.RoutesController.GenerateInvalid(true);

            //Act
            var act = async () => { await sut.Put(Guid.NewGuid(), _fixture.RouteViewModel.GenerateInvalid()); };

            //Assert
            act.Should().ThrowExactlyAsync<RouteNotFoundException>();
        }
    }
}
