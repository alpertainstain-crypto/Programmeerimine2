using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features
{
    public class GetUsersHandler : IRequestHandler<GetUser, OperationResult<PagedResult<User>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetUsersHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<User>>> Handle(GetUser request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<User>>();

            var query = _dbContext.Users.AsQueryable();

            // Apply search filters
            if (!string.IsNullOrWhiteSpace(request.SearchFirstName))
            {
                query = query.Where(x => x.FirstName.Contains(request.SearchFirstName));
            }

            if (!string.IsNullOrWhiteSpace(request.SearchLastName))
            {
                query = query.Where(x => x.LastName.Contains(request.SearchLastName));
            }

            if (!string.IsNullOrWhiteSpace(request.SearchEmail))
            {
                query = query.Where(x => x.Email.Contains(request.SearchEmail));
            }

            if (!string.IsNullOrWhiteSpace(request.SearchRole))
            {
                query = query.Where(x => x.Role == request.SearchRole);
            }

            result.Value = await query
                .OrderBy(x => x.LastName)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
