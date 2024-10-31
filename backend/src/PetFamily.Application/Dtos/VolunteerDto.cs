using PetFamily.Domain.Pet;
using PetFamily.Domain.Shared.Mails;
using PetFamily.Domain.Shared.PhoneNumber;
using PetFamily.Domain.Shared.Requisites;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer.Details;
using PetFamily.Domain.Volunteer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Dtos
{
    public class VolunteerDto
    {
        public string Name { get; init; }
        public string[] Requisites { get; init; }
        public string[]? Details { get; init; }
        public string Address { get; init; }

        public string Email { get; init; }
        public string Description { get; init; }
        public int Experience { get; init; }
        public string Number { get; init; }
        public PetDto[] Pets { get; init; }
    }
}
