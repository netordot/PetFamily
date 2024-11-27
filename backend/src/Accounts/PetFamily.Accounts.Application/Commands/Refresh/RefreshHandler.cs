using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using PetFamily.Accounts.Domain.DataModels;
using PetFamily.Application.AccountManagement.DataModels;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Application.Commands.Refresh
{
    public class RefreshHandler : ICommandHandler<JwtTokenResult,RefreshCommand>
    {
        private readonly IRefreshSessionManager _refreshSessionManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenProvider _tokenProvider;
        private readonly UserManager<User> _userManager;

        public RefreshHandler(
            IRefreshSessionManager refreshSessionManager,
            IUnitOfWork unitOfWork,
            ITokenProvider tokenProvider,
            UserManager<User> userManager
            )
        {
            _refreshSessionManager = refreshSessionManager;
            _unitOfWork = unitOfWork;
            _tokenProvider = tokenProvider;
            _userManager = userManager;
        }

        public async Task<Result<JwtTokenResult, ErrorList>> Handle(RefreshCommand command, CancellationToken cancellation)
        {
            // валидация 
            //  проверяем есть ли сессия
            // копируем сессию, старую сносим
            // проверяем не протухла ли сессия (проверяем уже копию)
            // генерим токенрезалт

            var session = await _refreshSessionManager.GetSessionByToken(command.refreshToken, cancellation);

            if(session.IsFailure)
            {
                return session.Error;
            }

            var sessionCopy = new RefreshSession()
            {
                CreatedAt = session.Value.CreatedAt,
                ExpiresAt = session.Value.ExpiresAt
            };

             _refreshSessionManager.DeleteSession(session.Value, cancellation);
            await _unitOfWork.SaveChanges(cancellation);

            if(sessionCopy.ExpiresAt < DateTime.UtcNow)
            {
                return Error.Conflict("expired.token", "token is expired").ToErrorList();
            }

            // сомневаюсь в необходимости данной проверки
            var user = await _userManager.FindByIdAsync(session.Value.UserId.ToString());
            if(user == null)
            {
                return Errors.General.NotFound(session.Value.UserId).ToErrorList();
            }

            var accessToken = _tokenProvider.GenerateAccessToken(user);
            var refreshToken = _tokenProvider.GenerateRefreshToken(user, cancellation);

            return new JwtTokenResult(accessToken, refreshToken.Result);
                
        }
    }
}
