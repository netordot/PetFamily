using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Accounts.Infrastructure.Data;
using PetFamily.Application.AccountManagement.DataModels;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Infrastructure.Managers
{
    public class RolePermissionManager
    {
        private readonly AccountsWriteDbContext _accountsDbContext;
        private readonly IUnitOfWork _unitOfWork;

        public RolePermissionManager(
            AccountsWriteDbContext accountsDbContext,
            [FromKeyedServices(ModuleNames.Accounts)] IUnitOfWork unitofWork)
        {
            _accountsDbContext = accountsDbContext;
            _unitOfWork = unitofWork;
        }

        public async Task CreateRolePermissionIfNotExists(string roleName, IEnumerable<string> permissions)
        {
            var role = await _accountsDbContext.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (role == null)
            {
                throw new ArgumentException($"role {roleName} is not found.");
            }

            foreach (var permission in permissions)
            {
                var permissionExists = await _accountsDbContext.Permissions
                    .FirstOrDefaultAsync(p => p.Code == permission);
                if (permissionExists == null)
                {
                    throw new ArgumentException($"permission {permission} does not exist");
                }

                var rolePermissionExists = await _accountsDbContext.RolePermissions
                    .AnyAsync(rp => rp.Permission.Code == permission && rp.Role.Name == roleName);
                if (rolePermissionExists)
                {
                    continue;
                }

                await _accountsDbContext.RolePermissions.AddAsync(new RolePermission()
                {
                    PermissionId = permissionExists.Id,
                    RoleId = role.Id
                });

            }

            await _accountsDbContext.SaveChangesAsync();
        }
    }
}
