using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Accounts.Contracts;
using PetFamily.Framework.Authorization.Attributes;
using PetFamily.SharedKernel.Constraints;

namespace PetFamily.Accounts.Infrastructure
{
    public class PermissionsRequirementHandler : AuthorizationHandler<PermissionRequirementAttribute>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PermissionsRequirementHandler(IServiceScopeFactory scopeFactory)
        {
            _serviceScopeFactory = scopeFactory;
        }
        protected override async Task HandleRequirementAsync
            (AuthorizationHandlerContext context, 
            PermissionRequirementAttribute permission)
        {
            var userId = context.User.FindFirst(u => u.Type == CustomClaims.Id);
            if(userId is null)
            {
                return;
            }

            var scope = _serviceScopeFactory.CreateScope();
            var accountsContract = scope.ServiceProvider.GetRequiredService<IAccountsContract>();

            var hasAccess = await accountsContract.CheckUserPermission(Guid.Parse(userId.Value), permission.Code);
            if (hasAccess)
            {   
                context.Succeed(permission);
            }

            return;
        }
    }
}
