using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Appointments
{
    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteAppointmentCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var appointment = await _dbContext.Appointments.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);
            if (appointment == null)
            {
                result.AddError("Appointment not found");
                return result;
            }

            _dbContext.Appointments.Remove(appointment);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
