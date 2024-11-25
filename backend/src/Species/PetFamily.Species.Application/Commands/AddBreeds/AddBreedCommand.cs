using PetFamily.Core.Abstractions;
using System.Windows.Input;

namespace PetFamily.Species.Application.Commands.AddBreeds
{
    public record AddBreedCommand(string name, Guid id) : Core.Abstractions.ICommand;

}