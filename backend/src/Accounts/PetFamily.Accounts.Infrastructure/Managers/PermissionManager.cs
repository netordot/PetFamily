using Microsoft.EntityFrameworkCore;
using PetFamily.Accounts.Infrastructure.Data;
using PetFamily.Application.AccountManagement.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Infrastructure.Managers
{
    public class PermissionManager
    {
        private readonly AccountsWriteDbContext _accountsDbContext;

        public PermissionManager(AccountsWriteDbContext accountsDbContext)
        {
            _accountsDbContext = accountsDbContext;
        }

        public async Task CreatePermissionIfNotExists(string permission)
        {
            var permissionExists = await _accountsDbContext.Permissions
                .AnyAsync(p => p.Code == permission);

            if(permissionExists == false)
            {
                await _accountsDbContext.Permissions.AddAsync(new Permission { Code = permission });
            }
        }

        public async Task<bool> CheckUserPermission(Guid userId,string permissionCode)
        {
            var roleNames = await _accountsDbContext.Users
                .Include(u => u.Roles)
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Roles)
                .Select(r => r.Name)
                .ToListAsync();

            if(roleNames.Count() == 0)
            {
                return false;
            }

            return await _accountsDbContext.RolePermissions.Where(rp => roleNames.Contains(rp.Role.Name))
                .FirstOrDefaultAsync(r => r.Permission.Code == permissionCode)!=null;
        }
    }
}
