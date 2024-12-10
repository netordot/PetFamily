using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Domain.DataModels;
using PetFamily.Application.AccountManagement.DataModels;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel.Constraints;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Application.Commands.Register
{
    public class RegisterUserHandler : ICommandHandler<Guid, RegisterUserCommand>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountManager _accountManager;

        public RegisterUserHandler(UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IAccountManager accountManager,
           [FromKeyedServices(ModuleNames.Accounts)] IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _accountManager = accountManager;
        }
        public async Task<Result<Guid, ErrorList>> Handle(RegisterUserCommand command, CancellationToken cancellation)
        {

            var existedUser = await _userManager.FindByEmailAsync(command.Email);
            if (existedUser != null)
            {
                return Errors.General.AlreadyExists(command.Email).ToErrorList();
            }

            var role = await _roleManager.FindByNameAsync(ParticipantAccount.PARTICIPANT);
            if (role == null)
            {
                return Errors.General.NotFound().ToErrorList();
            }

            var user = User.CreateParticipant(command.Email, role, command.UserName);
            if (user.IsFailure)
            {
                return user.Error.ToErrorList();
            }

            using IDbTransaction transaction = await _unitOfWork.BeginTransaction(cancellation);
            try
            {
                var result = await _userManager.CreateAsync(user.Value, command.Password);

                if (result.Succeeded == false)
                {
                    var errors = result.Errors.Select(e => Error.Failure(e.Code, e.Description)).ToList();

                    return new ErrorList(errors);
                }

                var perticipantAccount = new ParticipantAccount
                { FullName = user.Value.FullName, User = user.Value, Id = Guid.NewGuid() };

                user.Value.ParticipantAccount = perticipantAccount;
                await _accountManager.AddParticipantAccount(perticipantAccount);

                await _unitOfWork.SaveChanges(cancellation);

                transaction.Commit();

                return user.Value.Id;

            }

            catch
            {
                transaction.Rollback();
                return Error.Failure("transaction.failed", "transaction has failed").ToErrorList();
            }

            // дополнительно: подтвердить почту, подтвердить телефон, итп

        }
    }
}
