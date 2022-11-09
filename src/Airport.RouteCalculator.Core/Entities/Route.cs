namespace Airport.RouteCalculator.Core.Entities
{
    public class Route
    {
        public Guid Id { get; private set; }
        public string From { get; private set; }
        public string To { get; private set; }
        public decimal Value { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        protected Route() { }

        public Route(string from, string to, decimal value, IValidator<Route> validator)
        {
            Id = Guid.NewGuid();
            From = from;
            To = to;
            Value = value;
            CreatedAt = DateTime.Now;

            Validate(validator);
        }

        private void Validate(IValidator<Route> validator)
        {
            var validationResult = validator.Validate(this);

            if (validationResult.IsValid)
            {
                return;
            }

            throw new InvalidRouteException(validationResult.ToDictionary());
        }
    }
}
