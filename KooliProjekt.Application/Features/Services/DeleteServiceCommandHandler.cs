using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Services
{
    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteServiceCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var service = await _dbContext.Services.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);
            if (service == null)
            {
                result.AddError("Service not found");
                return result;
            }

            _dbContext.Services.Remove(service);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
