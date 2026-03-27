using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Services
{
    public class SaveServicesCommandHandler : IRequestHandler<SaveServicesCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveServicesCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveServicesCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var service = new Service();
            if (request.Id == 0)
            {
                await _dbContext.Services.AddAsync(service, cancellationToken);
            }
            else
            {
                service = await _dbContext.Services.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);
                if (service == null)
                {
                    result.AddError("Service not found");
                    return result;
                }
            }

            service.Code = request.Code;
            service.Description = request.Description;
            service.UnitPrice = request.UnitPrice;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
