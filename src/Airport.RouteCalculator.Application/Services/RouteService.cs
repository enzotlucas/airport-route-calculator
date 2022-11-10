namespace Airport.RouteCalculator.Application.Services
{
    public class RouteService : IRouteService
    {
        private readonly List<Route> _routes;
        private readonly List<BestRoutesOrder> _bestRoutes;
        private GetBestRouteQuery _request;       
        private BestRoutesOrder _bestRoutesOrder;

        public RouteService()
        {
            _routes = new List<Route>();
            _bestRoutes = new List<BestRoutesOrder>();
        }

        public string GetBestRoute(GetBestRouteQuery request, IEnumerable<Route> routes)
        {
            _request = request;

            _routes.AddRange(routes);

            GenerateBestRoutes();

            var bestRoute = _bestRoutes.OrderBy(b => b.TotalPrice).FirstOrDefault();

            return bestRoute.ToString();
        }

        private void GenerateBestRoutes()
        {
            var firstRouteCandidates = _routes.Where(r => r.From.Equals(_request.From, StringComparison.OrdinalIgnoreCase));

            Validate(firstRouteCandidates);

            foreach (var routeCandidate in firstRouteCandidates)
            {
                if (routeCandidate.To.Equals(_request.To, StringComparison.OrdinalIgnoreCase))
                {
                    _bestRoutes.Add(new BestRoutesOrder(routeCandidate));

                    continue;
                }

                StartBestRoutesGeneration(_routes, routeCandidate);
            }
        }

        private void Validate(IEnumerable<Route> firstRouteCandidates)
        {
            var lastRouteCandidates = _routes.Where(r => r.To.Equals(_request.To, StringComparison.OrdinalIgnoreCase));

            if (!lastRouteCandidates.Any())
            {
                throw new BusinessException("Não existem rotas cadastradas para esse destino");
            }

            if (!firstRouteCandidates.Any())
            {
                throw new BusinessException("Não existem rotas cadastradas que partem dessa origem");
            }
        }


        private void StartBestRoutesGeneration(IEnumerable<Route> routes, Route routeCandidate)
        {
            var secondRouteCandidates = routes.Where(r => r.From.Equals(routeCandidate.To));

            foreach (var nextRouteCandidate in secondRouteCandidates)
            {
                _bestRoutesOrder = new BestRoutesOrder(routeCandidate);

                _bestRoutesOrder.AddRoute(nextRouteCandidate);

                var nextRouteCandidates = _routes.Where(r => r.From.Equals(nextRouteCandidate.To, StringComparison.OrdinalIgnoreCase));

                GetNextRoute(nextRouteCandidates);
            }
        }

        private void GetNextRoute(IEnumerable<Route> routeCandidates)
        {
            foreach (var routeCandidate in routeCandidates)
            {
                var nextRouteCandidates = _routes.Where(r => r.From.Equals(routeCandidate.To, StringComparison.OrdinalIgnoreCase));

                if (_bestRoutesOrder.RouteAlreadyMapped(routeCandidate))
                {
                    return;
                }

                _bestRoutesOrder.AddRoute(routeCandidate);

                if (AddBestRouteIfMatches(routeCandidate))
                {
                    continue;
                }

                GetNextRoute(nextRouteCandidates);
            }
        }

        private bool AddBestRouteIfMatches(Route routeCandidate)
        {
            if (routeCandidate.To.Equals(_request.To, StringComparison.OrdinalIgnoreCase))
            {
                _bestRoutes.Add(_bestRoutesOrder);

                return true;
            }

            return false;
        }
    }
}
