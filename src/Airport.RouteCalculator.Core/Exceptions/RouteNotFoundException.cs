using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.RouteCalculator.Core.Exceptions
{
    public class RouteNotFoundException : BusinessException
    {
        public RouteNotFoundException(string message = "Rota não encontrada.") : base(message)
        {
        }
    }
}
