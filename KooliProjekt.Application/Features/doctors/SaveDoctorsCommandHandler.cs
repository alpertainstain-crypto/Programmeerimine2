using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.doctors;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features
{
    public class SaveDoctorsCommandHandler : IRequestHandler<SaveDoctorsCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveDoctorsCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveDoctorsCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var doctor = new Doctor();
            if (request.Id == 0)
            {
                await _dbContext.Doctors.AddAsync(doctor, cancellationToken);
            }
            else
            {
                doctor = await _dbContext.Doctors.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);
                if (doctor == null)
                {
                    result.AddError("Doktor not found");
                    return result;
                }
            }

            doctor.Name = request.title;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
