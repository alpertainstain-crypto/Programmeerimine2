using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features
{
    public class GetServices : IRequest<OperationResult<PagedResult<Service>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? SearchCode { get; set; }
        public string? SearchDescription { get; set; }
    }
}
