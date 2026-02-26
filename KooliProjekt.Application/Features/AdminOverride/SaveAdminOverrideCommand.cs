using System.Collections.Generic;
using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.AdminOverrideList
{
    public class SaveAdminOverrideCommand : IRequest<OperationResult>, ITransactional
    {
        public string Title { get; set; } = default!;
        public int Id { get; internal set; }
    }
}