using PetFamily.Core.Abstractions;
using System.Windows.Input;

namespace PetFamily.Application.Species.AddBreeds
{
    public record AddBreedCommand(string name, Guid id) : Core.Abstractions.ICommand;
    
}