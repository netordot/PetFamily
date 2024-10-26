namespace PetFamily.Application.Volunteers.UpdateRequisites;

public record UpdateRequisitesCommand(RequisiteListDto ListDto, Guid Id);