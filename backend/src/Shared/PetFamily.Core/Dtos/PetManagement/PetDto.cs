﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;

namespace PetFamily.Core.Dtos.PetManagement
{
    public class PetDto
    {
        public Guid Id { get; init; }
        public Guid VolunteerId { get; init; }
        public string Name { get; init; }
        public Guid SpeciesId { get; init; }
        public Guid BreedId { get; init; }
        public string Color { get; init; }
        public string Description { get; init; }
        public string HealthCondition { get; init; }
        public string PhoneNumber { get; init; }

        public string City { get; init; }
        public string Street { get; init; }
        public int BuildingNumber { get; init; }
        public int Status { get; init; }
        public double Height { get; init; }
        public double Weight { get; init; }
        public bool IsCastrated { get; init; }
        public bool IsVaccinated { get; init; }
        public DateTime DateOfBirth { get; init; }
        public DateTime CreatedAt { get; init; }
        public IEnumerable<RequisiteDto> Requisites { get; init; }
        public IEnumerable<ReadFileDto> Photos { get; init; }
        public int Position { get; init; }
    }
}
