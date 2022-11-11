namespace Airport.RouteCalculator.Core.Exceptions
{
    public sealed class RouteExistsException : BusinessException
    {
        public RouteExistsException(string message = "A rota já foi cadastrada.") : base(message)
        {
        }
    }
}
