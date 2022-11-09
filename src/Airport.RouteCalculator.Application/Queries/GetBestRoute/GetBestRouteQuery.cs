namespace Airport.RouteCalculator.Application.Queries.GetBestRoute
{
    public class GetBestRouteQuery : IRequest<string>
    {
        public string From { get; set; }
        public string To { get; set; }

        public GetBestRouteQuery(string from, string to)
        {
            From = from;
            To = to;
        }
    }
}
