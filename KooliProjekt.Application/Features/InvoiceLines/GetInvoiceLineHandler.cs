using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class GetInvoiceLineHandler : IRequestHandler<GetInvoiceLines, List<InvoiceLine>>
{
    private readonly ApplicationDbContext _db;

    public GetInvoiceLineHandler(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<InvoiceLine>> Handle(GetInvoiceLines request, CancellationToken token)
    {
        return await _db.InvoiceLine.ToListAsync(token);
    }
}
