using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class GetDoctorsHandler : IRequestHandler<GetDoctors, List<Doctor>>
{
    private readonly ApplicationDbContext _db;

    public GetDoctorsHandler(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<User>> Handle(GetUsers request, CancellationToken token)
    {
        return await _db.Users.ToListAsync(token);
    }
}
