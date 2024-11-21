using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Core.Dtos.PetManagement;
using PetFamily.CoreCore.Dtos.PetManagement;

namespace PetFamily.Core.Dtos.PetManagement
{
    public class VolunteerDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string MiddleName { get; init; }
        public string LastName { get; init; }

        public string City { get; init; }
        public string Street { get; init; }
        public int BuildingNumber { get; init; }
        public int? CorpsNumber { get; init; }
        public string Email { get; init; }
        public string Description { get; init; }
        public int Experience { get; init; }
        public string Number { get; init; }
        public IEnumerable<RequisiteDto> Requisites { get; init; }
        public IEnumerable<SocialDto> Socials { get; init; }

    }
}
