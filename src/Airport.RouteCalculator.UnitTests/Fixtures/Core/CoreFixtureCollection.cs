namespace Airport.RouteCalculator.UnitTests.Fixtures.Core
{
    [CollectionDefinition(nameof(CoreFixtureCollection))]
    public class CoreFixtureCollection : ICollectionFixture<CoreFixture> { }
    public class CoreFixture
    {
        public RouteFixture Route { get; private set; }

        public CoreFixture()
        {
            Route = new RouteFixture();
        }
    }
}
