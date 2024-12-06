using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Core.Dtos.Accounts
{
    public class PermissionDto
    {
        public Guid Id { get; set; }
        public string Code{ get; set; } = string.Empty;
        public List<RolePermissionDto> RolePermissions { get; set; } = [];
    }
}
