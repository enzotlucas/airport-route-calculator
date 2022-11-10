namespace Airport.RouteCalculator.API.Features.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/routes")]
    [Produces("application/json")]
    public class RoutesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoutesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RouteViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetRouteByIdQuery(id));

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RouteViewModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> Get(int? page, int? pageCount)
        {
            var response = await _mediator.Send(new GetRoutesQuery(page, pageCount));

            return Ok(response);
        }

        [HttpGet("best-cost")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetBestCostRoute(string from, string to)
        {
            var response = await _mediator.Send(new GetBestCostRouteQuery(from, to));

            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> Post(RouteViewModel route)
        {
            var response = await _mediator.Send(new CreateRouteCommand(route));

            return CreatedAtAction(nameof(Post), response);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteRouteCommand(id));

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> Put(Guid id, RouteViewModel route)
        {
            await _mediator.Send(new UpdateRouteCommand(id, route));

            return NoContent();
        }
    }
}
