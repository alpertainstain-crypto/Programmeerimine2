using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Services
{
    public class SaveServicesCommandHandler : IRequestHandler<SaveServicesCommand, OperationResult>
    {
        private readonly IServiceRepository _serviceRepository;

        public SaveServicesCommandHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<OperationResult> Handle(SaveServicesCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            Service service;
            if (request.Id == 0)
            {
                service = new Service();
                await _serviceRepository.AddAsync(service, cancellationToken);
            }
            else
            {
                service = await _serviceRepository.GetByIdAsync(request.Id, cancellationToken);
                if (service == null)
                {
                    result.AddError("Service not found");
                    return result;
                }
            }

            service.Code = request.Code;
            service.Description = request.Description;
            service.UnitPrice = request.UnitPrice;

            await _serviceRepository.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
