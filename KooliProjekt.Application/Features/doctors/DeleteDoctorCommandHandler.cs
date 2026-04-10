using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.doctors
{
    public class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand, OperationResult>
    {
        private readonly IDoctorRepository _doctorRepository;

        public DeleteDoctorCommandHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<OperationResult> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var doctor = await _doctorRepository.GetByIdAsync(request.Id, cancellationToken);
            if (doctor == null)
            {
                result.AddError("Doktor not found");
                return result;
            }

            await _doctorRepository.DeleteAsync(request.Id, cancellationToken);
            await _doctorRepository.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
