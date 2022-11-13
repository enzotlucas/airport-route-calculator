using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airport.RouteCalculator.UnitTests.Core.Exceptions
{
    public class RouteNotFoundExceptionTests
    {
        [Fact]
        public void Constructor_DefaultMessage_ShouldThrow()
        {
            //Arrange & Act
            var act = () => { throw new RouteNotFoundException(); };

            //Assert
            act.Should().ThrowExactly<RouteNotFoundException>()
                        .WithMessage("Rota não encontrada.");
        }

        [Fact]
        public void Constructor_CustomMessage_ShouldThrow()
        {
            //Arrange
            var message = "Route not found custom";

            //Act
            var act = () => { throw new RouteNotFoundException(message); };

            //Assert
            act.Should().ThrowExactly<RouteNotFoundException>()
                        .WithMessage(message);
        }
    }
}
