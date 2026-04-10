using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Appointments
{
    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, OperationResult>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public DeleteAppointmentCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<OperationResult> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var appointment = await _appointmentRepository.GetByIdAsync(request.Id, cancellationToken);
            if (appointment == null)
            {
                result.AddError("Appointment not found");
                return result;
            }

            await _appointmentRepository.DeleteAsync(request.Id, cancellationToken);
            await _appointmentRepository.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
