using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class GetAvailabilityHandler : IRequestHandler<GetAvailability, List<Availability>>
{
    private readonly ApplicationDbContext _db;

    public GetAvailabilityHandler(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<Availability>> Handle(GetAvailability request, CancellationToken token)
    {
        return await _db.Availability.ToListAsync(token);
    }
}
