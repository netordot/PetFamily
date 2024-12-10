using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Core.Dtos.Accounts
{
    public class RoleDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
    }
}
