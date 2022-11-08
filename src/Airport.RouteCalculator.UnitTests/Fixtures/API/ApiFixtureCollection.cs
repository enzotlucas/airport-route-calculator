namespace Airport.RouteCalculator.UnitTests.Fixtures.API
{
    [CollectionDefinition(nameof(ApiFixtureCollection))]
    public class ApiFixtureCollection : ICollectionFixture<ApiFixture> { }

    public class ApiFixture : IDisposable
    {
        public RoutesControllerFixture RoutesController { get; private set; }
        public RouteViewModelFixture RouteViewModel { get; private set; }

        public ApiFixture()
        {
            RoutesController = new();
            RouteViewModel = new();
        }

        public void Dispose()
        {

        }
    }
}
