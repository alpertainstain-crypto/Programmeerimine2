using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features
{
    public class GetDoctorsHandler : IRequestHandler<GetDoctors, OperationResult<PagedResult<Doctor>>>
    {
        private readonly IDoctorRepository _doctorRepository;

        public GetDoctorsHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<OperationResult<PagedResult<Doctor>>> Handle(GetDoctors request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Doctor>>();

            var doctors = await _doctorRepository.GetAllAsync(cancellationToken);
            var pagedResult = doctors
                .OrderBy(x => x.Name)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            result.Value = new PagedResult<Doctor>
            {
                Items = pagedResult,
                TotalCount = doctors.Count,
                Page = request.Page,
                PageSize = request.PageSize
            };

            return result;
        }
    }
}
