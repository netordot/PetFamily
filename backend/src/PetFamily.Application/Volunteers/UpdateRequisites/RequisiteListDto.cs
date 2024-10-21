using PetFamily.Domain;
using PetFamily.Domain.Shared.Requisites;

namespace PetFamily.Application.Volunteers.UpdateRequisites;

public record RequisiteListDto(List<Requisite> requisites);