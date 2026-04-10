using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.doctors;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features
{
    public class SaveDoctorsCommandHandler : IRequestHandler<SaveDoctorsCommand, OperationResult>
    {
        private readonly IDoctorRepository _doctorRepository;

        public SaveDoctorsCommandHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<OperationResult> Handle(SaveDoctorsCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            Doctor doctor;
            if (request.Id == 0)
            {
                doctor = new Doctor();
                await _doctorRepository.AddAsync(doctor, cancellationToken);
            }
            else
            {
                doctor = await _doctorRepository.GetByIdAsync(request.Id, cancellationToken);
                if (doctor == null)
                {
                    result.AddError("Doktor not found");
                    return result;
                }
            }

            doctor.Name = request.title;

            await _doctorRepository.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
