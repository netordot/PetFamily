using PetFamily.Core.Dtos.PetManagement;
using PetFamily.CoreCore.Dtos.PetManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Core.Dtos.Accounts
{
    public class VolunteerAccountDto
    {
        public static string VOLUNTEER = "Volunteer";
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public UserDto User { get; set; }
        public int Experience { get; set; }
        public IEnumerable<RequisiteDto> Requisites { get; init; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}
