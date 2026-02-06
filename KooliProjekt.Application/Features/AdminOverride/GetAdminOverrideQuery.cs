using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.AdminOverride
{
    public class GetAdminOverride : IRequest<OperationResult<object>>
    {
        public int Id { get; set; }
    }
}