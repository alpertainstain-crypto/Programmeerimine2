using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features
{
    public class GetAvailabilityQuery : IRequest<OperationResult<object>>
    {
        public int Id { get; set; }
        public int Page { get; internal set; }
        public int PageSize { get; internal set; }
    }
}
