using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Volunteers.Application.Commands.UpdateRequisites;

public record RequisiteListDto(List<Requisite> requisites);