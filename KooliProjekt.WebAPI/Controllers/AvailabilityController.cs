using System.Threading.Tasks;
using KooliProjekt.Application.Features.TodoLists;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class AvailabilityController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public AvailabilityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var query = new GetAvailability();
            var result = await _mediator.Send(query);

            return Result(result);
        }
    }
}
