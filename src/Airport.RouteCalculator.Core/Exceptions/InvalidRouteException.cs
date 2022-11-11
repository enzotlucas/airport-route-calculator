namespace Airport.RouteCalculator.Core.Exceptions
{
    public sealed class InvalidRouteException : BusinessException
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
