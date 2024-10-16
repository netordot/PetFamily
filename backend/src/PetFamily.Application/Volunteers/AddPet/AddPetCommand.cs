using PetFamily.Domain;
using PetFamily.Domain.Shared.PhoneNumber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.AddPet
{
    public record AddPetCommand
    (
        Guid VolunteerId,
        // другие файлы и дто по аналогии с волонтером
        string Name,
        //SpeciesBreedDto SpeciesBreed,
        string Color,
        string Description,
        string HealthCondition,
        PetStatus status,
        double Weight,
        double Height,
        bool IsCastrated,
        bool IsVaccinated,
        IEnumerable<FileDto> Files,
        DateTime BirthDate

    );

    public record SpeciesBreedDto(SpeciesBreed SpeciesBreed);

    public record FileDto(string fileName);
}
