using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Infrastructure.Options
{
    public class RolePermissionOptions
    {
        public Dictionary<string, string[]> Permissions { get; set; }  
        public Dictionary<string, string[]> Roles { get; set; } 

    }
}
