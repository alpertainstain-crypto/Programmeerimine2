using System;
using System.Collections.Generic;
using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.AdminOverrideList
{
    public class SaveAdminOverrideCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int DoctorId { get; set; }
        public int CreatedBy { get; set; }
    }
}