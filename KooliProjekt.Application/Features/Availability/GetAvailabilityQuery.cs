using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Availability
{
    public class GetAvailability : IRequest<OperationResult<object>>
    {
        public int Id { get; set; }
        public int Page { get; internal set; }
        public int PageSize { get; internal set; }
    }
}