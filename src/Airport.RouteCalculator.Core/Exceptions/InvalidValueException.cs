namespace Airport.RouteCalculator.Core.Exceptions
{
    public class InvalidValueException : BusinessException
    {
        public InvalidValueException(string message = "O valor da rota está inválido") : base(message)
        {
        }
    }
}
