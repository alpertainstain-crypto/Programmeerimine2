using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class GetUsersHandler : IRequestHandler<GetUsers, List<User>>
{
    private readonly ApplicationDbContext _db;

    public GetUsersHandler(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<User>> Handle(GetUsers request, CancellationToken token)
    {
        return await _db.Users.ToListAsync(token);
    }
}
