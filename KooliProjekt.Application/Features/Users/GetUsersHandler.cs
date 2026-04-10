using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features
{
    public class GetUsersHandler : IRequestHandler<GetUser, OperationResult<PagedResult<User>>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult<PagedResult<User>>> Handle(GetUser request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<User>>();

            var users = await _userRepository.GetAllAsync(cancellationToken);
            var pagedResult = users
                .OrderBy(x => x.Name)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            result.Value = new PagedResult<User>
            {
                Items = pagedResult,
                TotalCount = users.Count,
                Page = request.Page,
                PageSize = request.PageSize
            };

            return result;
        }
    }
}
