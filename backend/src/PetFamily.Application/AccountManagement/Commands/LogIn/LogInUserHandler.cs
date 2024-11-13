using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using PetFamily.Application.Abstractions;
using PetFamily.Application.AccountManagement.DataModels;
using PetFamily.Application.Authorization;
using PetFamily.Domain.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.AccountManagement.Commands.LogIn
{
    public class LogInUserHandler : ICommandHandler<string, LogInUserCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenProvider _tokenProvier;

        public LogInUserHandler(UserManager<User> userManager, ITokenProvider tokenProvider)
        {
            _userManager = userManager;
            _tokenProvier = tokenProvider;
        }


        public async Task<Result<string, ErrorList>> Handle(LogInUserCommand command, CancellationToken cancellation)
        {
            var user = await _userManager.FindByEmailAsync(command.Email);
            if (user == null)
            {
                return Errors.User.InvalidCredentials().ToErrorList();
            }

            var passwordConfirmed = await _userManager.CheckPasswordAsync(user, command.Password);
            if (!passwordConfirmed)
            {
                return Errors.User.InvalidCredentials().ToErrorList();

            }

            var token = _tokenProvier.GenerateAccessToken(user);

            return token;
        }
    }
}
