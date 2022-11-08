namespace Airport.RouteCalculator.Application.Commands.CreateRoute
{
    public class CreateRouteCommand : IRequest<RouteViewModel>
    {
        public string From { get; set; }
        public string To { get; set; }
        public decimal Value { get; set; }

        public CreateRouteCommand(RouteViewModel routeViewModel)
        {
            From = routeViewModel.Origem;
            To = routeViewModel.Destino;
            Value = routeViewModel.Valor;
        }
    }
}
