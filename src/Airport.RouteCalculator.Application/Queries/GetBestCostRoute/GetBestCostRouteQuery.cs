namespace Airport.RouteCalculator.Application.Queries.GetBestCostRoute
{
    public class GetBestCostRouteQuery : IRequest<string>
    {
        public string From { get; set; }
        public string To { get; set; }

        public GetBestCostRouteQuery(string from, string to)
        {
            From = from;
            To = to;
        }
    }
}
