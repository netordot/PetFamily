using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Domain.DataModels;
using PetFamily.Application.AccountManagement.DataModels;
using PetFamily.Core.Abstractions;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.AccountManagement.Commands.LogIn
{
    public class LogInUserHandler : ICommandHandler<JwtTokenResult, LogInUserCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenProvider _tokenProvier;
        private readonly IRefreshSessionManager _refreshSessionManager;

        public LogInUserHandler(
            UserManager<User> userManager,
            ITokenProvider tokenProvider,
            IRefreshSessionManager refreshSessionManager)
        {
            _userManager = userManager;
            _tokenProvier = tokenProvider;
            _refreshSessionManager = refreshSessionManager; 
        }

        public async Task<Result<JwtTokenResult, ErrorList>> Handle(
            LogInUserCommand command, 
            CancellationToken cancellation)
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

            var accessToken = _tokenProvier.GenerateAccessToken(user);

            var refreshSession = await _refreshSessionManager.GetSessionByUserId(user.Id, cancellation);
            if(refreshSession.IsSuccess)
            {
                _refreshSessionManager.DeleteSession(refreshSession.Value, cancellation);
            }

            var refreshToken = await _tokenProvier.GenerateRefreshToken(user, cancellation);

            var token = new JwtTokenResult(accessToken, refreshToken);

            return token;
        }
    }
}
