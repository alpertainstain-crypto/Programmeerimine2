using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.User
{
    public class GetUsersHandler : IRequestHandler<GetUser, OperationResult<PagedResult<User>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetUsersHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<User>>> Handle(GetUsers request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<User>>();

            result.Value = await _dbContext
                .User
                .OrderBy(x => x.Name)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
