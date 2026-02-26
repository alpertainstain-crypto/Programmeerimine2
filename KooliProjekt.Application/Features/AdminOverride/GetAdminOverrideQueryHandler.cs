using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.AdminOverrideList
{
    public class GetAdminOverrideQueryHandler : IRequestHandler<GetAdminOverride, OperationResult<object>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetAdminOverrideQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<object>> Handle(GetAdminOverride request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<object>();

            result.Value = await _dbContext
                .AdminOverride
                .Include(list => list.Doctor)
                .Where(list => list.Id == request.Id)
                .Select(list => new
                {
                    Id = list.Id,
                    Reason = list.Reason,
                    Start = list.Start,
                    End = list.End
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}