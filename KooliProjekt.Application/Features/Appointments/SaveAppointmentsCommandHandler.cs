using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.AdminOverrideList;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Appointments
{
    internal class SaveAppointmentsCommandHandler: IRequestHandler<SaveAdminOverrideCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        private OperationResult result;

        public SaveAppointmentsCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveAppointmentsCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            Appointment appointment;

            if (request.Id == 0)
            {
                appointment = new Appointment();
                appointment.Title = request.Title;

                await _dbContext.Appointments.AddAsync(appointment);
            }
            else
            {
                appointment = await _dbContext.Appointments.FindAsync(request.Id);

                if (appointment == null)
                {
                    result.AddError("Appointment not found");
                    return result;
                }

                appointment.Title = request.Title;
            }

            await _dbContext.SaveChangesAsync();

            return result;
        }

        public Task<OperationResult> Handle(SaveAdminOverrideCommandHandler request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Handle(SaveAdminOverrideCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
