using PetFamily.Accounts.Contracts;
using PetFamily.Accounts.Infrastructure.Managers;
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

        public AccountsContract(PermissionManager permission)
        {
            _permissionManager = permission;
        }

        public async Task<bool> CheckUserPermission(Guid userId, string permissionCode)
        {
            return await _permissionManager.CheckUserPermission(userId, permissionCode);
        }
    }
}
