using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Availability
{
    public class SaveAvailabilityCommand : IRequest<OperationResult>, ITransactional
    {
        public string title { get; set; } = default!;
        public object Title { get; internal set; }
        public int Id { get; internal set; }
    }
}