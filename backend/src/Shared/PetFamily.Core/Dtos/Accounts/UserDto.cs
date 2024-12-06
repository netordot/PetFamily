using PetFamily.CoreCore.Dtos.PetManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Core.Dtos.Accounts
{
    public class UserDto
    {
        public Guid Id { get; init; }
        public string FristName { get; init; } = string.Empty;
        public string MiddleName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public IEnumerable<SocialDto> SocialNewroks { get; set; } = [];
        public List<RoleDto> Roles { get; set; } = [];
        public VolunteerAccountDto? VolunteerAccount { get; set; }
        public AdminAccountDto? AdminAccount { get; set; }
        public ParticipantAccountDto? ParticipantAccount { get; set; }

    }
}
