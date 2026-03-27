using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Services
{
    public class ServicesQuery : IRequest<OperationResult<PagedResult<Service>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
