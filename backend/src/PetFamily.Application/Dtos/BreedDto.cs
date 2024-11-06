using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Dtos
{
    public class BreedDto
    {
        public Guid  Id { get; init; }
        public Guid SpeciesId { get; set; }

        public BreedDto(Guid id, Guid speciesId)
        {
            Id = id;
            SpeciesId = speciesId;
        }

        private BreedDto()
        {
            
        }
    }
}
