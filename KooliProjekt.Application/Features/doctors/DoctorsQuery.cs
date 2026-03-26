using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features
{
    public class DoctorsQuery : IRequest<OperationResult<PagedResult<global::Doctor>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}