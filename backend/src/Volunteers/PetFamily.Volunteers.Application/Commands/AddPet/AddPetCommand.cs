using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos.PetManagement;

namespace PetFamily.Application.PetManagement.Commands.Volunteers.AddPet
{
    public record AddPetCommand
    (
        Guid VolunteerId,
        // другие файлы и дто по аналогии с волонтером
        string Name,
        Guid SpeciesId,
        Guid BreedId,
        string Color,
        string Description,
        string HealthCondition,
        // поменял petstatus, проверить
        PetStatusDto status,
        double Weight,
        double Height,
        bool IsCastrated,
        bool IsVaccinated,
        DateTime BirthDate,
        List<RequisiteDto> Requisites

    ) : ICommand;
}
