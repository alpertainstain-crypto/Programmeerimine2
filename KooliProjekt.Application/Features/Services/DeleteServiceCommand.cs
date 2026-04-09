using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Services
{
    public class DeleteServiceCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
    }
}
