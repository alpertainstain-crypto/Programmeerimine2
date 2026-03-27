using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Features.AdminOverrideList;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features
{
	public class SaveAdminOverrideCommandHandler : IRequestHandler<SaveAdminOverrideCommand, OperationResult>
	{
		private readonly ApplicationDbContext _dbContext;

		public SaveAdminOverrideCommandHandler(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<OperationResult> Handle(SaveAdminOverrideCommand request, CancellationToken cancellationToken)
		{
			var result = new OperationResult();

			var adminOverride = new AdminOverride();
			if (request.Id == 0)
			{
				await _dbContext.AdminOverride.AddAsync(adminOverride, cancellationToken);
			}
			else
			{
				adminOverride = await _dbContext.AdminOverride.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);
				if (adminOverride == null)
				{
					result.AddError("Admin Override not found");
					return result;
				}
			}

			adminOverride.Reason = request.Title ?? adminOverride.Reason;
			adminOverride.Start = request.Start ?? adminOverride.Start;
			adminOverride.End = request.End ?? adminOverride.End;
			adminOverride.DoctorId = request.DoctorId > 0 ? request.DoctorId : adminOverride.DoctorId;
			adminOverride.CreatedBy = request.CreatedBy > 0 ? request.CreatedBy : adminOverride.CreatedBy;

			await _dbContext.SaveChangesAsync(cancellationToken);

			return result;
		}
	}
}
