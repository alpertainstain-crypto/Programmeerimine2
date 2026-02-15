using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.AdminOverride
{
       public class SaveAdminOverrideCommand : IRequest<OperationResult>, ITransactional
    {
        [AdminOverrideTitle]
        public string Title { get; set; } = default!;

        public List<SaveAdminOverrideItemDto> Items { get; set; } = new();
    }
}