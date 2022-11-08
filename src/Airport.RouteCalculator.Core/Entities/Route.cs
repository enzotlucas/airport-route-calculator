using Airport.RouteCalculator.Core.Exceptions;

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

        public Route(string from, string to, decimal value)
        {
            Id = Guid.NewGuid();
            From = from;
            To = to;
            Value = value;
            CreatedAt = DateTime.Now;

            Validate();
        }

        private void Validate()
        {
            if (From.Equals(To, StringComparison.OrdinalIgnoreCase))
                throw new BusinessException("A origem não pode ser igual ao destino.");

            if (Value < 1)
                throw new BusinessException("O valor da viagem não pode ser abaixo de 1.");
        }
    }
}
