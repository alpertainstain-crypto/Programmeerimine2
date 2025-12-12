using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VisiteDocument
{
    public class GetVisitDocumentHandler : IRequestHandler<GetVisitDocument, OperationResult<PagedResult<VisitDocument>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetVisitDocumentHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<VisitDocument>>> Handle(GetVisitDocument request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<VisitDocument>>();

            result.Value = await _dbContext
                .VisitDocuments
                .OrderByDescending(x => x.Id)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
