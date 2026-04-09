using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.doctors
{
    public class DeleteDoctorCommand : IRequest<OperationResult>, ITransactional
    {
        public int Id { get; set; }
    }
}
