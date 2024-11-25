using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.AccountManagement.DataModels
{
    public class Role : IdentityRole<Guid>
    {
        public List<RolePermission> RolePermissions { get; set; }
    }
}
