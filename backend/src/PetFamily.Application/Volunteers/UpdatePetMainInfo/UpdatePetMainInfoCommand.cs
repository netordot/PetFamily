using PetFamily.Application.Abstractions;
using PetFamily.Application.Volunteers.SharedDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.UpdatePetMainInfo
{
    public record UpdatePetMainInfoCommand 
        (
        Guid VolunteerId,
        Guid PetId,
        string Name,
        Guid SpeciesId,
        Guid BreedId,
        string PhoneNumber,
        string Color,
        string Description,
        string HealthCondition,
        string City,
        string Street,
        int BuildingNumber,
        int CorpsNumber,
        // пока что статус будет доменным, когда будет фронт, избавиться от доменна в реквесте
        Domain.Pet.PetStatus status,
        double Weight,
        double Height,
        bool IsCastrated,
        bool IsVaccinated,
        DateTime BirthDate,
        List<RequisiteDto> Requisites
        ) : ICommand;
    
}
