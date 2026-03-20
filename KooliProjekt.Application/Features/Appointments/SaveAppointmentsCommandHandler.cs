using KooliProjekt.Application.Data;
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
    internal class SaveAppointmentsCommandHandler: IRequestHandler<SaveAdminOverrideCommandHandler, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        private OperationResult result;

        public SaveAppointmentsCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

            public async Task<OperationResult> Handle(SaveAppointmentsCommand request, CancellationToken cancellationToken)
             {
             var list = new OperationResult();

            var page = new Appointment();
            if (request.Id == 0)
                {
                await _dbContext.Appointments.AddAsync(list);
            }
            else
                {
                    page = await _dbContext.Appointments.FindAsync(request.Id);
               
                list.Title = request.Title;

                await _dbContext.SaveChangesAsync();

                return result;
            }
        }

        public Task<OperationResult> Handle(SaveAdminOverrideCommandHandler request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
