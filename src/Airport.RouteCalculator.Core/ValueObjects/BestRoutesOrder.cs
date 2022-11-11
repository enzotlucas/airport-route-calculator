namespace Airport.RouteCalculator.Core.ValueObjects
{
    public sealed class BestRoutesOrder
    {
        public List<BestRoute> BestRoutes { get; set; }
        public decimal TotalPrice => BestRoutes.Sum(b => b.Route.Value);

        public BestRoutesOrder()
        {
            BestRoutes = new List<BestRoute>();
        }

        public BestRoutesOrder(Route route)
        {
            BestRoutes = new List<BestRoute>()
            {
                new BestRoute(route, 0)
            };
        }

        public void AddRoute(Route route)
        {
            var lastPosition = BestRoutes.Count;

            BestRoutes.Add(new BestRoute(route, lastPosition));
        }

        public bool RouteAlreadyMapped(Route route)
        {
            return BestRoutes.Any(r => r.Route.Id == route.Id);
        }

        public bool RouteIsValid(string to)
        {
            return BestRoutes.FirstOrDefault(b => b.Position == BestRoutes.Count - 1)
                             .Route
                             .To
                             .Equals(to, StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            var response = string.Empty;

            for (int i = 0; i < BestRoutes.Count; i++)
            {
                var route = BestRoutes.FirstOrDefault(b => b.Position == i);

                if (i == BestRoutes.Count - 1)
                {
                    return $"{response}{route.Route.From} - {route.Route.To} ao custo de ${TotalPrice}";
                }

                response = $"{response}{route.Route.From} - ";
            }

            return response;
        }
    }
}
