using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.RouteCalculator.UnitTests.Fixtures.Application.Commands
{
    public class UpdateRouteCommandFixture
    {
        public UpdateRouteCommandHandler GenerateValidHandler(Route route)
        {
            var uow = Substitute.For<IUnitOfWork>();
            uow.Routes.GetByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult(route));
            uow.SaveChangesAsync().Returns(Task.FromResult(true));

            var logger = Substitute.For<ILogger<UpdateRouteCommandHandler>>();

            return new UpdateRouteCommandHandler(uow, logger);
        }

        public UpdateRouteCommandHandler GenerateInvalidHandler(Route route)
        {
            var uow = Substitute.For<IUnitOfWork>();
            uow.Routes.GetByIdAsync(Arg.Any<Guid>()).Returns(Task.FromResult(route));
            uow.SaveChangesAsync().Returns(Task.FromResult(false));

            var logger = Substitute.For<ILogger<UpdateRouteCommandHandler>>();

            return new UpdateRouteCommandHandler(uow, logger);
        }

        public UpdateRouteCommand GenerateCommandFromViewModel(RouteViewModel routeViewModel)
        {
            return new UpdateRouteCommand(routeViewModel.Id, routeViewModel);
        }
    }
}
