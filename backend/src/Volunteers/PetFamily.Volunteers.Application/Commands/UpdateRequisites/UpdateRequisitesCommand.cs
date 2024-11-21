using PetFamily.Core.Abstractions;
using PetFamily.Volunteers.Application.Commands.UpdateRequisites;
using System.Windows.Input;

namespace PetFamily.Volunteers.Application.Commands.UpdateRequisites;

public record UpdateRequisitesCommand(RequisiteListDto ListDto, Guid Id) : Core.Abstractions.ICommand;