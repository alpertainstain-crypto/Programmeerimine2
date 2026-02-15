using System;
using System.linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.AdminOverride
{
    public class GetAdminOverrideQueryHandler : IRequestHandler<AdminOverrideQuery, OperationResult<object>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAdminOverrideQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(GetAdminOverrideQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();

            result.Value = await _dbContext
                .AdminOverrides
                .Include(list => list.Items)
                .Where(list => list.Id == request.Id)
                .Select(list => new
                {
                    Id = list.Id,
                    Title = list.Title,
                    Items = list.Items.Select(item => new
                    {
                        item.Id,
                        item.Title
                    })
                })
                .FirstOrDefaultAsync();

            return result;
        }
    }
}