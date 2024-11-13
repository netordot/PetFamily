using Microsoft.AspNetCore.Authorization;
using PetFamily.Infrastructure.Authentication.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.Authentication.Providers
{
    public class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return Task.FromResult<AuthorizationPolicy>(new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build());
    
        }

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        {
            return Task.FromResult<AuthorizationPolicy?>(null!);
        }

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        { 
            if(string.IsNullOrWhiteSpace(policyName))
            {
                return Task.FromResult<AuthorizationPolicy?>(null);
            }
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddRequirements(new PermissionRequirementAttribute(policyName))
                .Build();

            return Task.FromResult<AuthorizationPolicy?>(policy);
        }
    }
}
