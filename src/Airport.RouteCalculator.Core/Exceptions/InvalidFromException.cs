namespace Airport.RouteCalculator.Core.Exceptions
{
    public class InvalidFromException : BusinessException
    {
        public InvalidFromException(string message = "A origem da rota está inválida") : base(message)
        {
        }
    }
}
