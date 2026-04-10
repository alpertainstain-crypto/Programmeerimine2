using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Availability
{
    public class DeleteAvailabilityCommandHandler : IRequestHandler<DeleteAvailabilityCommand, OperationResult>
    {
        private readonly IAvailabilityRepository _availabilityRepository;

        public DeleteAvailabilityCommandHandler(IAvailabilityRepository availabilityRepository)
        {
            _availabilityRepository = availabilityRepository;
        }

        public async Task<OperationResult> Handle(DeleteAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var availability = await _availabilityRepository.GetByIdAsync(request.Id, cancellationToken);
            if (availability == null)
            {
                result.AddError("Availability not found");
                return result;
            }

            await _availabilityRepository.DeleteAsync(request.Id, cancellationToken);
            await _availabilityRepository.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
