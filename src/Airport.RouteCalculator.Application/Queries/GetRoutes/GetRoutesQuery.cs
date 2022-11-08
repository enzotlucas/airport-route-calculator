namespace Airport.RouteCalculator.Application.Queries.GetRoutes
{
    public class GetRoutesQuery : IRequest<IEnumerable<RouteViewModel>>
    {
        public int? Page { get; set; }
        public int? Rows { get; set; }

        public GetRoutesQuery(int? page, int? rows)
        {
            Page = page;
            Rows = rows;
        }
    }
}
