using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.doctors
{
    public class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteDoctorCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var doctor = await _dbContext.Doctors.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);
            if (doctor == null)
            {
                result.AddError("Doktor not found");
                return result;
            }

            _dbContext.Doctors.Remove(doctor);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
