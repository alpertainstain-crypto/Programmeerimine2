using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;

namespace KooliProjekt.Application.Features
{
    public class AppointmentsQuery : IRequest<OperationResult<PagedResult<Appointment>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public DateTime? SearchFromDate { get; set; }
        public DateTime? SearchToDate { get; set; }
        public int? SearchDoctorId { get; set; }
        public int? SearchUserId { get; set; }
    }
}
