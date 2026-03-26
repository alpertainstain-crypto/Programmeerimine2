using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.AdminOverrideList;
using KooliProjekt.Application.Features.doctors;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace KooliProjekt.Application.Features
{
    public class SaveDoctorsCommandHandler : IRequestHandler<SaveDoctorsCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveDoctorsCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveDoctorsCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var list = new Doctor();
            if (request.Id == 0)
            {
                await _dbContext.Doctors.AddAsync(list);
            }
            else
            {
                list = await _dbContext.Doctors.FindAsync(request.Id);
            }

            list.Title = request.Title;

            await _dbContext.SaveChangesAsync();

            return result;
        }
    }
}
