namespace Airport.RouteCalculator.Core.Exceptions
{
    public sealed class RouteNotFoundException : BusinessException
    {
        public RouteNotFoundException(string message = "Rota não encontrada.") : base(message)
        {
        }
    }
}
