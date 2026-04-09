using System.Threading.Tasks;
using KooliProjekt.Application.Features;
using KooliProjekt.Application.Features.doctors;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class DoctorsController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public DoctorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> List([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = new DoctorsQuery { Page = page, PageSize = pageSize };
            var result = await _mediator.Send(query);

            return Result(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteDoctorCommand { Id = id };
            var result = await _mediator.Send(command);

            return Result(result);
        }
    }
}
