using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class GetInvoiceHandler : IRequestHandler<GetInvoice, List<Invoice>>
{
    private readonly ApplicationDbContext _db;

    public GetInvoiceHandler(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<Invoice>> Handle(GetInvoice request, CancellationToken token)
    {
        return await _db.Invoice.ToListAsync(token);
    }
}
