using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Contracts;
using PetFamily.Accounts.Infrastructure.Managers;
using PetFamily.Application.AccountManagement.DataModels;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel.Constraints;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Presentation
{

    public class AccountsContract : IAccountsContract
    {
        private readonly PermissionManager _permissionManager;
        private readonly UserManager<User> _userManager;
        private readonly IVolunteerAccountManager _accountManager;
        private readonly IUnitOfWork _unitOfWork;

        public AccountsContract(PermissionManager permission,
            UserManager<User> userManager,
            IVolunteerAccountManager accountManager,
            [FromKeyedServices(ModuleNames.Accounts)]IUnitOfWork unitOfWork)
        {
            _permissionManager = permission;
            _userManager = userManager; 
            _accountManager = accountManager;
            _unitOfWork = unitOfWork;   
        }

        public async Task<bool> CheckUserPermission(Guid userId, string permissionCode)
        {
            return await _permissionManager.CheckUserPermission(userId, permissionCode);
        }

        public async Task<Result<Guid, ErrorList>> CreateVolunteerAccount(
            Guid userId,
            int experience,
            List<Requisite> requisites,
            CancellationToken cancellation)
        {
            var userExists = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellation);
            if (userExists == null)
            {
                return Errors.General.NotFound(userId).ToErrorList();
            }

            var isVolunteer = userExists.Roles.Any(r => r.Name == "Volunteer");
            if(isVolunteer == true)
            {
                return Error.Conflict("is.volunteer", "user is already a volunteer").ToErrorList();
            }

            var result = await _userManager.AddToRoleAsync(userExists, "Volunteer");
            if(result.Succeeded == false)
            {
                return Error.Failure("role.error", "unable to provide role").ToErrorList();
            }

            var volunteerAccount = new VolunteerAccount(userId,Guid.NewGuid(), userExists, experience, userExists.FullName, requisites);

            await _accountManager.Add(volunteerAccount, cancellation);

            await _unitOfWork.SaveChanges(cancellation);

            return volunteerAccount.Id;
            
        }
    }
}
