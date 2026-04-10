using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Availability
{
    public class SaveAvailabilityCommandHandler : IRequestHandler<SaveAvailabilityCommand, OperationResult>
    {
        private readonly IAvailabilityRepository _availabilityRepository;
        private OperationResult result;

        public SaveAvailabilityCommandHandler(IAvailabilityRepository availabilityRepository)
        {
            _availabilityRepository = availabilityRepository;
            result = new OperationResult();
        }

        public async Task<OperationResult> Handle(SaveAvailabilityCommand request, CancellationToken cancellationToken)
        {
            global::Availability list;

            if (request.Id == 0)
            {
                list = new global::Availability { DoctorId = 1 };
                await _availabilityRepository.AddAsync(list, cancellationToken);
            }
            else
            {
                list = await _availabilityRepository.GetByIdAsync(request.Id, cancellationToken);
                if (list != null)
                {
                    list.DoctorId = 1;
                }
            }

            await _availabilityRepository.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}