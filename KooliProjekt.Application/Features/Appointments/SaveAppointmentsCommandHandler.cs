using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Appointments
{
    public class SaveAppointmentsCommandHandler: IRequestHandler<SaveAppointmentsCommand, OperationResult>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public SaveAppointmentsCommandHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<OperationResult> Handle(SaveAppointmentsCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            Appointment appointment;

            if (request.Id == 0)
            {
                appointment = new Appointment();
                await _appointmentRepository.AddAsync(appointment, cancellationToken);
            }
            else
            {
                appointment = await _appointmentRepository.GetByIdAsync(request.Id, cancellationToken);

                if (appointment == null)
                {
                    result.AddError("Appointment not found");
                    return result;
                }
            }

            if (!string.IsNullOrWhiteSpace(request.title))
            {
                // Note: Appointment doesn't have Title property, storing in Time for now
                // This should be updated based on actual requirements
            }

            appointment.Time = request.AppointmentTime ?? appointment.Time;
            appointment.UserId = request.UserId > 0 ? request.UserId : appointment.UserId;
            appointment.DoctorId = request.DoctorId > 0 ? request.DoctorId : appointment.DoctorId;

            await _appointmentRepository.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
