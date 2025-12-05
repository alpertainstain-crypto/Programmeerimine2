using KooliProjekt.Application.Data;
using MediatR;
using System.Collections.Generic;

public class GetAppointments : IRequest<List<Appointment>>
{
}

