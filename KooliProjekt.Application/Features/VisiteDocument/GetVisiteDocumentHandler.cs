using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.VisiteDocument
{
    public class GetVisiteDocumentHandler : IRequestHandler<GetVisiteDocument, OperationResult<PagedResult<VisiteDocument>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetVisiteDocumentHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<VisiteDocument>>> Handle(GetVisiteDocument request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<VisiteDocument>>();

            result.Value = await _dbContext
                .VisiteDocuments
                .OrderBy(x => x.Name)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}
