using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.ToDoList
{
    public class GetToDoListQueryHandler : IRequestHandler<GetToDoListQuery, OperationResult<object>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetToDoListQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<OperationResult<object>> Handle(GetToDoListQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var result = new OperationResult<object>();

            if (request.Id <= 0)
            {
                result.Value = null;
                return result;
            }

            result.Value = await _dbContext
                .ToDoLists
                .Where(list => list.Id == request.Id)
                .Select(list => new
                {
                    Id = list.Id,
                    Title = list.Title,
                    Description = list.Description,
                    IsCompleted = list.IsCompleted,
                    CreatedDate = list.CreatedDate,
                    DueDate = list.DueDate
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
