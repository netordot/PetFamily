using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Dtos
{
    public class SpeciesDto
    {
        public Guid Id { get; init; }
        public IEnumerable<BreedDto> Breeds { get; init; }
        public string Name { get; init; }

        public SpeciesDto(Guid id, IEnumerable<BreedDto> breeds, string name)
        {
            Id = id;
            Breeds = breeds;
            Name = name;
        }

        private SpeciesDto()
        {
            
        }

    }
}
