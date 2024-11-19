using Microsoft.AspNetCore.Authorization;
using PetFamily.Accounts.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.Authentication
{
    public class PermissionsRequirementHandler : AuthorizationHandler<PermissionRequirementAttribute>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirementAttribute permission)
        {
            var userPermission = context.User.Claims.FirstOrDefault(c => c.Type == "Permission");
            if (userPermission == null)
            {
                return;
            }

            if(userPermission.Value == permission.Code)
            {
                context.Succeed(permission);

            }
        }
    }
}
