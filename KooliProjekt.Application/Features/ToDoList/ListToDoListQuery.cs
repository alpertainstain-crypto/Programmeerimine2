using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.ToDoList
{
    public class ListToDoListQuery : IRequest<OperationResult<PagedResult<object>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
