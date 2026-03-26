using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Availability
{
    public class SaveAvailabilityCommandHandler : IRequestHandler<SaveAvailabilityCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;
        private OperationResult result;

        public SaveAvailabilityCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            result = new OperationResult();
        }

        public async Task<OperationResult> Handle(SaveAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var list = new global::Availability();

            if (request.Id == 0)
            {
                list.DoctorId = 1;
                await _dbContext.Availability.AddAsync(list);
            }
            else
            {
                list = await _dbContext.Availability.FindAsync(request.Id);
                if (list != null)
                {
                    list.DoctorId = 1;
                }
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}