using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.RouteCalculator.UnitTests.Fixtures.Application.Queries
{
    public class GetBestRouteQueryFixture
    {
        public GetBestCostRouteQueryHandler GenerateValidHandler(IEnumerable<Route> routes, string response)
        {
            var uow = Substitute.For<IUnitOfWork>();
            uow.Routes.GetAllAsync().Returns(routes);

            var service = Substitute.For<IRouteService>();
            service.GetBestCostRoute(Arg.Any<GetBestCostRouteQuery>(), routes).Returns(response);

            var logger = Substitute.For<ILogger<GetBestCostRouteQueryHandler>>();

            return new GetBestCostRouteQueryHandler(uow, service, logger);
        }
    }
}
