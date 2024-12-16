using PetFamily.Core.Dtos.PetManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Core.Dtos.VolunteerRequest
{
    public record VolunteerInfoDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Experience { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Description { get; set; } = String.Empty;
        public List<RequisiteDto> Requisites { get; set; }
        public VolunteerRequestStatusDto Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
