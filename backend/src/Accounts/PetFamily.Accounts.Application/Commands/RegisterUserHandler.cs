using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using PetFamily.Application.AccountManagement.DataModels;
using PetFamily.Core.Abstractions;
using PetFamily.Domain.Shared.Errors;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.AccountManagement.Commands
{
    public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly UserManager<User> _userManager;

        public RegisterUserHandler(UserManager<User> userManager)
        {
            _userManager = userManager; 
        }
        public async Task<UnitResult<ErrorList>> Handle(RegisterUserCommand command, CancellationToken cancellation)
        {
            // проверить что нет такого пользователя

            var existedUser = await _userManager.FindByEmailAsync(command.Email);
            if (existedUser != null)
            {
                return Errors.General.AlreadyExists(command.Email).ToErrorList();
            }

            var user = new User 
            { Email = command.Email,
              UserName = command.UserName
            };

            var result = await _userManager.CreateAsync(user, command.Password);

            if(result.Succeeded == false)
            {
                var errors = result.Errors.Select(e => Error.Failure(e.Code, e.Description)).ToList();

                return new ErrorList(errors);
            }
            // дополнительно: подтвердить почту, подтвердить телефон, итп

            return Result.Success<ErrorList>();
        }
    }
}
