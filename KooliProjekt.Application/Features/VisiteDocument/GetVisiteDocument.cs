using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VisiteDocument
{
    public class GetVisiteDocument : IRequest<OperationResult<PagedResult<VisiteDocument>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
