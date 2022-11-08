namespace Airport.RouteCalculator.Core.Exceptions
{
    public class InfrastructureException : Exception
    {
        public InfrastructureException(string message, Exception innerException = null) 
            : base(message, innerException)
        {
        }
    }
}
