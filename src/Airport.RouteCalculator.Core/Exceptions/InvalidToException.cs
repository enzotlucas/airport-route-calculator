namespace Airport.RouteCalculator.Core.Exceptions
{
    public class InvalidToException : BusinessException
    {
        public InvalidToException(string message = "O destino da rota está inválido") : base(message)
        {
        }
    }
}
