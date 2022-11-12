namespace Airport.RouteCalculator.UnitTests.Fixtures.Application
{
    [CollectionDefinition(nameof(ApplicationFixtureCollection))]
    public class ApplicationFixtureCollection : ICollectionFixture<ApplicationFixture> { }

    public class ApplicationFixture
    {
        public RouteViewModelFixture RouteViewModel { get; private set; }
        public RouteFixture Route { get; private set; }

        public CreateRouteCommandFixture CreateRouteCommand { get; private set; }
        public DeleteRouteCommandFixture DeleteRouteCommand { get; private set; }
        public UpdateRouteCommandFixture UpdateRouteCommand { get; private set; }

        public ApplicationFixture()
        {
            RouteViewModel = new RouteViewModelFixture();
            Route = new RouteFixture();

            CreateRouteCommand = new CreateRouteCommandFixture();
            DeleteRouteCommand = new DeleteRouteCommandFixture();
            UpdateRouteCommand = new UpdateRouteCommandFixture();
        }
    }
}
