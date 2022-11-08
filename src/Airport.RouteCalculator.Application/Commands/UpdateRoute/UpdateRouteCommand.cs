namespace Airport.RouteCalculator.Application.Commands.UpdateRoute
{
    public class UpdateRouteCommand : IRequest
    {
        public Guid Id { get; set; }

        public string From { get; set; }
        public string To { get; set; }
        public decimal Value { get; set; }

        public UpdateRouteCommand(Guid id, RouteViewModel routeViewModel)
        {
            Id = id;
            From = routeViewModel.Origem;
            To = routeViewModel.Destino;
            Value = routeViewModel.Valor;
        }
    }
}
