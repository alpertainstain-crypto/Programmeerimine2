using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;

namespace KooliProjekt.Application.Features
{
    public class InvoiceQuery : IRequest<OperationResult<PagedResult<Invoice>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? SearchStatus { get; set; }
        public DateTime? SearchFromDate { get; set; }
        public DateTime? SearchToDate { get; set; }
    }
}
