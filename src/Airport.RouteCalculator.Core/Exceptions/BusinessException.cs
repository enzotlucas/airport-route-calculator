namespace Airport.RouteCalculator.Core.Exceptions
{
    public class BusinessException : Exception
    {
        public IDictionary<string, string[]> ValidationErrors;

        public BusinessException(string message) : base(message)
        {
            ValidationErrors = new Dictionary<string, string[]>();
        }

        public BusinessException(IDictionary<string, string[]> validationErrors, string message)
            : base(message)
        {
            ValidationErrors = validationErrors;
        }
    }
}
