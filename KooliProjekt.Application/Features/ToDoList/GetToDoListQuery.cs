using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.ToDoList
{
    public class GetToDoListQuery : IRequest<OperationResult<object>>
    {
        public int Id { get; set; }
    }
}
