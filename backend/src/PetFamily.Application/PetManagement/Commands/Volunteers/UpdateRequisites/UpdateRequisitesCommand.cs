using PetFamily.Core.Abstractions;
using System.Windows.Input;

namespace PetFamily.Application.PetManagement.Commands.Volunteers.UpdateRequisites;

public record UpdateRequisitesCommand(RequisiteListDto ListDto, Guid Id) : Core.Abstractions.ICommand;