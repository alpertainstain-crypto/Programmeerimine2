using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features
{
    public class GetUser : IRequest<OperationResult<PagedResult<User>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? SearchFirstName { get; set; }
        public string? SearchLastName { get; set; }
        public string? SearchEmail { get; set; }
        public string? SearchRole { get; set; }
    }
}
