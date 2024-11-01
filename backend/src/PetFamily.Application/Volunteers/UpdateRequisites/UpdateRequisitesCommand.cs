using PetFamily.Application.Abstractions;
using System.Windows.Input;

namespace PetFamily.Application.Volunteers.UpdateRequisites;

public record UpdateRequisitesCommand(RequisiteListDto ListDto, Guid Id) : Abstractions.ICommand;