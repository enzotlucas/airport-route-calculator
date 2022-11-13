namespace Airport.RouteCalculator.Core.Entities
{
    public sealed class Route
    {
        public Guid Id { get; private set; }
        public string From { get; private set; }
        public string To { get; private set; }
        public decimal Value { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public bool IsValid => Id != Guid.Empty;

        public Route(string from, string to, decimal value, IValidator<Route> validator)
        {
            Id = Guid.NewGuid();
            From = from?.ToUpper();
            To = to?.ToUpper();
            Value = value;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;

            Validate(validator);
        }

        public Route()
        {
            Id = Guid.Empty;
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

        public void Update(string from, string to, decimal? value)
        {
            UpdateFrom(from);

            UpdateTo(to);

            UpdateValue(value);
        }

        private void UpdateFrom(string from)
        {
            if(from is null || from.Equals(From, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            if(string.IsNullOrWhiteSpace(from) || from.Length != 3 || from.Equals(To, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidFromException();
            }

            From = from.ToUpper();
        }

        private void UpdateTo(string to)
        {
            if (to is null || to.Equals(To, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(to) || to.Length != 3 || to.Equals(From, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidToException();
            }

            To = to.ToUpper();
        }

        private void UpdateValue(decimal? value)
        {
            if (value is null || value.Value == Value)
            {
                return;
            }

            if(value < 1)
            {
                throw new InvalidValueException();
            }

            Value = value.Value;
        }
    }
}
