using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features
{
    public class AdminOverrideQuery : IRequest<OperationResult<PagedResult<AdminOverride>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Id { get; internal set; }
    }
}
