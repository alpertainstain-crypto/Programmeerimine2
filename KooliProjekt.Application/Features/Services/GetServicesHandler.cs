using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class GetServicesHandler : IRequestHandler<GetServices, List<Service>>
{
    private readonly ApplicationDbContext _db;

    public GetServicesHandler(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<Service>> Handle(GetServices request, CancellationToken token)
    {
        return await _db.Services.ToListAsync(token);
    }
}
