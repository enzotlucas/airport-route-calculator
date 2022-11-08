namespace Airport.RouteCalculator.Application.Commands.DeleteRoute
{
    public class DeleteRouteCommand : IRequest
    {
        public Guid Id { get; set; }

        public DeleteRouteCommand(Guid id)
        {
            Id = id;
        }
    }
}
