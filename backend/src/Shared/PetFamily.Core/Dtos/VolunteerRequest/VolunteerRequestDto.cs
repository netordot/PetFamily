using PetFamily.Core.Dtos.PetManagement;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Core.Dtos.VolunteerRequest
{
    public class VolunteerRequestDto
    {
        public Guid Id { get; set; }
        public Guid DiscussionId { get; set; }
        public Guid AdminId { get; set; }
        public Guid UserId { get; set; }
        public string RejectionComment { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty ;
        public string SecondName { get; set; } = string.Empty ;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Experience { get; set; }
        public string Email { get; set; } = string.Empty ;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Description { get; set; } = String.Empty ;
        public List<RequisiteDto> Requisites { get; set; }
        public VolunteerRequestStatusDto Status { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
