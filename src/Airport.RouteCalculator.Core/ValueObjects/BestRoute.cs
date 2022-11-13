namespace Airport.RouteCalculator.Core.ValueObjects
{
    public sealed class BestRoute
    {
        public Route Route { get; set; }
        public int Position { get; set; }

        public BestRoute(Route route, int position)
        {
            Route = route;
            Position = position;
        }
    }
}
