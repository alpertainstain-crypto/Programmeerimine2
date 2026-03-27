using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Users
{
    public class SaveUsersCommandHandler : IRequestHandler<SaveUsersCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public SaveUsersCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(SaveUsersCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            var user = new User();
            if (request.Id == 0)
            {
                user.CreatedAt = DateTime.Now;
                await _dbContext.Users.AddAsync(user, cancellationToken);
            }
            else
            {
                user = await _dbContext.Users.FindAsync(new object[] { request.Id }, cancellationToken: cancellationToken);
                if (user == null)
                {
                    result.AddError("User not found");
                    return result;
                }
            }

            user.Name = request.Name;
            user.Email = request.Email;
            user.Phone = request.Phone;
            user.PasswordHash = request.PasswordHash;
            user.Role = request.Role;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
