using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PetFamily.Accounts.Application;
using PetFamily.Accounts.Infrastructure.Managers;
using PetFamily.Accounts.Infrastructure.Options;
using PetFamily.Application.AccountManagement.DataModels;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel.Constraints;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Infrastructure.Seeding
{
    public class AdminAccountsSeederService
    {
        private readonly PermissionManager _permissionManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountManager _accountManager;
        private readonly IAccountManager accountManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly RolePermissionManager _rolePermissionManager;
        private readonly AdminOptions _adminOptions;
        private readonly UserManager<User> _userManager;

        public AdminAccountsSeederService(
            PermissionManager permissionManager,
            RoleManager<Role> roleManager,
            RolePermissionManager rolePermissionManager,
            IOptions<AdminOptions> adminOptions,
            [FromKeyedServices(ModuleNames.Accounts)] IUnitOfWork unitOfWork,
            IAccountManager accountManager,
            UserManager<User> userManager)
        {
            _permissionManager = permissionManager;
            _unitOfWork = unitOfWork;
            _accountManager = accountManager;
            _roleManager = roleManager;
            _rolePermissionManager = rolePermissionManager;
            _adminOptions = adminOptions.Value;
            _userManager = userManager;
        }

        public async Task Seed(CancellationToken cancellation)
        {
            var json = await File.ReadAllTextAsync("etc/accounts.json");

            var data = JsonSerializer.Deserialize<RolePermissionOptions>(json)
                ?? throw new Exception("Null reference role permissions config");

            await SeedPermissions(data, cancellation);
            await SeedRoles(data, cancellation);
            await SeedRolePermissions(data, cancellation);
            await SeedAdmin(cancellation);

        }

        private async Task SeedAdmin(CancellationToken cancellation)
        {
            var adminRole = await _roleManager.FindByNameAsync(AdminOptions.ADMIN)
                ?? throw new Exception("Can not find admin role");

            var user = User.CreateAdmin(_adminOptions.Email, adminRole, _adminOptions.UserName);
            if(user.IsFailure)
            {
                throw new ArgumentException("Failed to create user");
            }

            await _userManager.CreateAsync(user.Value, _adminOptions.Password);

            var fullName = new FullName("admin", "admin", "admin");

             await _accountManager.AddAdminAccount
                (new AdminAccount() { FullName = fullName, User = user.Value, Id = Guid.NewGuid() });

            await _unitOfWork.SaveChanges(cancellation);
              
            
        }

        private async Task SeedRolePermissions(RolePermissionOptions options, CancellationToken cancellation)
        {
            foreach(var role in options.Roles.Keys)
            {
                await _rolePermissionManager.CreateRolePermissionIfNotExists(role, options.Roles[role]); 
            }

            await _unitOfWork.SaveChanges(cancellation);
        }

        public async Task SeedPermissions(RolePermissionOptions options, CancellationToken cancellation)
        {
            foreach (var permissionTemp in options.Permissions.Values)
            {
                foreach (var permission in permissionTemp)
                {
                    await _permissionManager.CreatePermissionIfNotExists(permission);
                }
            }

            await _unitOfWork.SaveChanges(cancellation);
        }

        public async Task SeedRoles(RolePermissionOptions options, CancellationToken cancellation)
        {
            foreach (var role in options.Roles.Keys)
            {
             var result = await _roleManager.FindByNameAsync(role);
                if(result == null)
                {
                    await _roleManager.CreateAsync(new Role() { Name = role});
                }

                await _unitOfWork.SaveChanges(cancellation);
            }
        }


    }
}
