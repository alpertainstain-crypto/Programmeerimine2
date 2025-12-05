using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class GetAdminOverrideHandler : IRequestHandler<GetAdminOverrride, List<AdminOverride>>
{
    private readonly ApplicationDbContext _db;

    public GetAdminOverrideHandler(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<AdminOverride>> Handle(GetAdminOverrride request, CancellationToken token)
    {
        return await _db.AdminOverride.ToListAsync(token);
    }
}
