namespace Airport.RouteCalculator.Application.Queries.GetRouteById
{
    public class GetRouteByIdQuery : IRequest<RouteViewModel>
    {
        public Guid Id { get; set; }

        public GetRouteByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
