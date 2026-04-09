using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Availability
{
    public class DeleteAvailabilityCommandHandler : IRequestHandler<DeleteAvailabilityCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteAvailabilityCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var availability = await _dbContext.Availability.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);
            if (availability == null)
            {
                result.AddError("Availability not found");
                return result;
            }

            _dbContext.Availability.Remove(availability);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
