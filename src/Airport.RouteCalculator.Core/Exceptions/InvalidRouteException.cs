namespace Airport.RouteCalculator.Core.Exceptions
{
    public class InvalidRouteException : BusinessException
    {
        public InvalidRouteException(string message = "Rota inválida.") : base(message)
        {
        }
    }
}
