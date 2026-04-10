using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Services
{
    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, OperationResult>
    {
        private readonly IServiceRepository _serviceRepository;

        public DeleteServiceCommandHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<OperationResult> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var service = await _serviceRepository.GetByIdAsync(request.Id, cancellationToken);
            if (service == null)
            {
                result.AddError("Service not found");
                return result;
            }

            await _serviceRepository.DeleteAsync(request.Id, cancellationToken);
            await _serviceRepository.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
