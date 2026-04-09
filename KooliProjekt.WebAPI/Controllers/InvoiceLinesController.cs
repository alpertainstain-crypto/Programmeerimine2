using System.Threading.Tasks;
using KooliProjekt.Application.Features;
using KooliProjekt.Application.Features.InvoiceLines;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class InvoiceLinesController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public InvoiceLinesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> List([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetInvoiceLines { Page = page, PageSize = pageSize };
            var result = await _mediator.Send(query);

            return Result(result);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] SaveInvoiceLinesCommand command)
        {
            var result = await _mediator.Send(command);

            return Result(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteInvoiceLineCommand { Id = id };
            var result = await _mediator.Send(command);

            return Result(result);
        }
    }
}
