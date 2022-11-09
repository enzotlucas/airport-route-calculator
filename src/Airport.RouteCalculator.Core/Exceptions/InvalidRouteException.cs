namespace Airport.RouteCalculator.Core.Exceptions
{
    public class InvalidRouteException : BusinessException
    {
        public InvalidRouteException(string message = "Rota inválida.") : base(message)
        {
        }

        public InvalidRouteException(IDictionary<string, string[]> validationErrors,
                                     string message = "Rota inválida") 
            : base(validationErrors, message)
        {
        }
    }
}
