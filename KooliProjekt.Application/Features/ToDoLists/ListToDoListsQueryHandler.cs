using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.ToDoLists
{
    public class ListToDoListsQueryHandler : IRequestHandler<ListToDoListsQuery, OperationResult<PagedResult<ToDoList>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListToDoListsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<ToDoList>>> Handle(ListToDoListsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<ToDoList>>();

            result.Value = await _dbContext
                .ToDoLists
                .OrderBy(list => list.Title)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}