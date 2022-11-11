namespace Airport.RouteCalculator.Application.ViewModels
{
    public sealed class ErrorResponseViewModel
    {
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("errors")]
        public IDictionary<string, string[]> Errors { get; set; }

        public ErrorResponseViewModel(Exception exception)
        {
            Message = exception.Message;
            Errors = new Dictionary<string, string[]>();
        }

        public ErrorResponseViewModel(BusinessException exception)
        {
            Message = exception.Message;
            Errors = exception.ValidationErrors;
        }
    }
}
