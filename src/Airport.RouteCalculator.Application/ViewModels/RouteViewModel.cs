namespace Airport.RouteCalculator.Application.ViewModels
{
    public class RouteViewModel
    {
        public Guid Id { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public decimal Valor { get; set; }
    }
}
