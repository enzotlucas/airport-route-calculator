using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.RouteCalculator.Core.Exceptions
{
    public class RouteExistsException : BusinessException
    {
        public RouteExistsException(string message = "A rota já foi cadastrada.") : base(message)
        {
        }
    }
}
