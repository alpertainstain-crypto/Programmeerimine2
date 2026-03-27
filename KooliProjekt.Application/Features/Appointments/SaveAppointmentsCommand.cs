using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Appointments
{
    public class SaveAppointmentsCommand: IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public DateTime? AppointmentTime { get; set; }
        public int UserId { get; set; }
        public int DoctorId { get; set; }
        public string title { get; set; } = default!;
    }
}
