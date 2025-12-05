using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class GetAppointmentsHandler : IRequestHandler<GetAppointments, List<Appointment>>
{
    private readonly ApplicationDbContext _db;

    public GetAppointmentsHandler(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<Appointment>> Handle(GetAppointments request, CancellationToken token)
    {
        return await _db.Appointment.ToListAsync(token);
    }
}
