using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.AdminOverrideList
{
    public class DeleteAdminOverrideCommandHandler : IRequestHandler<DeleteAdminOverrideCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteAdminOverrideCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteAdminOverrideCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var adminOverride = await _dbContext.AdminOverride.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);
            if (adminOverride == null)
            {
                result.AddError("Admin override not found");
                return result;
            }

            _dbContext.AdminOverride.Remove(adminOverride);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
