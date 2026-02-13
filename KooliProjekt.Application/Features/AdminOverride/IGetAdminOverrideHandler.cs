using System.Threading;

namespace KooliProjekt.Application.Features
{
    public interface IGetAdminOverrideHandler
    {
        System.Threading.Tasks.Task<Infrastructure.Results.OperationResult<Infrastructure.Paging.PagedResult<global::AdminOverride>>> Handle(AdminOverride request, CancellationToken cancellationToken);
    }
}