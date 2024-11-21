using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Framework.Attributes
{
    public class PermissionRequirementAttribute : AuthorizeAttribute, IAuthorizationRequirement
    {
        public string Code { get; }
        public PermissionRequirementAttribute(string code) : base(policy: code)
        {
            Code = code;
        }
    }
}
