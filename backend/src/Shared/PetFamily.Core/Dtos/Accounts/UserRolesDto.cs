using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Core.Dtos.Accounts
{
    public class UserRolesDto
    {
        public Guid UserId { get; set; }
        public UserDto User { get; set; } = default!;
        public Guid RoleId { get; set; }
        public RoleDto Role { get; set; } = default!;
    }
}
