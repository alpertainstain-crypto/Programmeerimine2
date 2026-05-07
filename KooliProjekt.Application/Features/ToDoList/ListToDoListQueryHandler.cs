using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.ToDoList
{
    public class ListToDoListQueryHandler : IRequestHandler<ListToDoListQuery, OperationResult<PagedResult<object>>>
    {
        private readonly ApplicationDbContext _dbContext;
        private const int MaxPageSize = 100;

        public ListToDoListQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<OperationResult<PagedResult<object>>> Handle(ListToDoListQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Page <= 0)
            {
                throw new ArgumentException("Page must be greater than zero.", nameof(request.Page));
            }

            if (request.PageSize <= 0)
            {
                throw new ArgumentException("PageSize must be greater than zero.", nameof(request.PageSize));
            }

            if (request.PageSize > MaxPageSize)
            {
                throw new ArgumentException($"PageSize cannot exceed {MaxPageSize}.", nameof(request.PageSize));
            }

            var result = new OperationResult<PagedResult<object>>();

            var query = _dbContext.ToDoLists.AsQueryable();

            var pagedResult = await query
                .OrderByDescending(list => list.CreatedDate)
                .Select(list => new
                {
                    Id = list.Id,
                    Title = list.Title,
                    Description = list.Description,
                    IsCompleted = list.IsCompleted,
                    CreatedDate = list.CreatedDate,
                    DueDate = list.DueDate
                })
                .GetPagedAsync(request.Page, request.PageSize);

            result.Value = new PagedResult<object>
            {
                CurrentPage = pagedResult.CurrentPage,
                PageCount = pagedResult.PageCount,
                PageSize = pagedResult.PageSize,
                RowCount = pagedResult.RowCount,
                Results = pagedResult.Results.Cast<object>().ToList()
            };

            return result;
        }
    }
}
