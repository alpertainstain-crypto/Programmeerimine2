using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Availability
{
    public class DeleteAvailabilityCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
    }
}
