using PetFamily.Domain.Pet.PetPhoto;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Shared.PhoneNumber;
using PetFamily.Domain.Shared.Requisites;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Dtos
{
    public class PetDto
    {
        public Guid VolunteerId { get; init; }
        public string Name { get; init; }
        public Guid SpeciesId { get; init; }
        public Guid BreedId { get; init; }
        public string Color { get; init; }
        public string Description { get; init; }
        public string HealthCondition { get; init; }
        public string PhoneNumber { get; init; }
        //public Address Address { get; private set; }
        public string Address { get; init; }
        public int Status { get; init; }

        public double Height { get; init; }
        public double Weight { get; init; }
        public bool IsCastrated { get; init; }
        public bool IsVaccinated { get; init; }
        public DateTime DateOfBirth { get; init; }
        public DateTime CreatedAt { get; init; }
        public string[] Requisites { get; init; }
        public string Photos { get; init; }
        public int Position { get; init; }
    }
}
