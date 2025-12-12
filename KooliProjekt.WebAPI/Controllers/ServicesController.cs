using System.Threading.Tasks;
using KooliProjekt.Application.Features;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class ServicesController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public ServicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var query = new GetServices();
            var result = await _mediator.Send(query);

            return Result(result);
        }
    }
}
