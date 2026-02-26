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
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

			var list = new AdminOverride();
			if (request.Id == 0)
			{
				await _dbContext.AdminOverride.AddAsync(list);
			}
			else
			{
				list = await _dbContext.AdminOverride.FindAsync(request.Id);
			}

			list.Title = request.Title;

			await _dbContext.SaveChangesAsync();

			return result;
		}
	}
}
