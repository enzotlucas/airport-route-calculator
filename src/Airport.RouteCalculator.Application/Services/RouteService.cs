namespace Airport.RouteCalculator.Application.Services
{
    public sealed class RouteService : IRouteService
    {
        private readonly List<Route> _routes;
        private readonly List<BestRoutesOrder> _bestRoutes;
        private GetBestCostRouteQuery _request;       
        private BestRoutesOrder _bestRoutesOrder;

        public RouteService()
        {
            _routes = new List<Route>();
            _bestRoutes = new List<BestRoutesOrder>();
        }

        public string GetBestCostRoute(GetBestCostRouteQuery request, IEnumerable<Route> routes)
        {
            _request = request;

            _routes.AddRange(routes);

            GenerateBestRoutes();
            
            var bestRoute = _bestRoutes.OrderBy(b => b.TotalPrice).FirstOrDefault(b => b.RouteIsValid(_request.To));

            if(bestRoute is null)
            {
                throw new BusinessException("Não existe rota que vá da origem ao destino informado.");
            }

            return bestRoute.ToString();
        }

        private void GenerateBestRoutes()
        {
            var firstRouteCandidates = _routes.Where(r => r.From.Equals(_request.From, StringComparison.OrdinalIgnoreCase));

            Validate(firstRouteCandidates);

            StartBestRoutesGeneration(firstRouteCandidates);
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

        private void StartBestRoutesGeneration(IEnumerable<Route> firstRouteCandidates)
        {
            foreach (var routeCandidate in firstRouteCandidates)
            {
                if (routeCandidate.To.Equals(_request.To, StringComparison.OrdinalIgnoreCase))
                {
                    _bestRoutes.Add(new BestRoutesOrder(routeCandidate));

                    continue;
                }

                GetNextRoutesCandidates(routeCandidate);
            }
        }

        private void GetNextRoutesCandidates(Route routeCandidate)
        {
            var secondRouteCandidates = _routes.Where(r => r.From.Equals(routeCandidate.To));

            foreach (var nextRouteCandidate in secondRouteCandidates)
            {
                _bestRoutesOrder = new BestRoutesOrder(routeCandidate);

                _bestRoutesOrder.AddRoute(nextRouteCandidate);

                if (!GenerateNextRoutesCandidatesIfBestRouteDoesntMatch(nextRouteCandidate, out var nextRouteCandidates))
                {
                    continue;
                }    

                GetNextRoute(nextRouteCandidates);
            }
        }

        private bool GenerateNextRoutesCandidatesIfBestRouteDoesntMatch(Route nextRouteCandidate, out IEnumerable<Route> nextRouteCandidates)
        {
            if(AddBestRouteIfMatches(nextRouteCandidate))
            {
                nextRouteCandidates = Enumerable.Empty<Route>();

                return false;
            }

            nextRouteCandidates = _routes.Where(r => r.From.Equals(nextRouteCandidate.To, StringComparison.OrdinalIgnoreCase));

            return true;
        }

        private void GetNextRoute(IEnumerable<Route> routeCandidates)
        {
            foreach (var routeCandidate in routeCandidates)
            {
                if (_bestRoutesOrder.RouteAlreadyMapped(routeCandidate))
                {
                    return;
                }

                _bestRoutesOrder.AddRoute(routeCandidate);

                if (!GenerateNextRoutesCandidatesIfBestRouteDoesntMatch(routeCandidate, out var nextRouteCandidates))
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
