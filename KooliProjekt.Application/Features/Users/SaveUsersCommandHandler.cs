using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Users
{
    public class SaveUsersCommandHandler : IRequestHandler<SaveUsersCommand, OperationResult>
    {
        private readonly IUserRepository _userRepository;

        public SaveUsersCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult> Handle(SaveUsersCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            User user;
            if (request.Id == 0)
            {
                user = new User { CreatedAt = DateTime.Now };
                await _userRepository.AddAsync(user, cancellationToken);
            }
            else
            {
                user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
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

            await _userRepository.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
