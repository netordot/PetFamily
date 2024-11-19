using PetFamily.Domain;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Application.PetManagement.Commands.Volunteers.UpdateRequisites;

public record RequisiteListDto(List<Requisite> requisites);