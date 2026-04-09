using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.AdminOverrideList
{
    public class DeleteAdminOverrideCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
    }
}
