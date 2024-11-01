using PetFamily.Application.Abstractions;
using System.Windows.Input;

namespace PetFamily.Application.Species.AddBreeds
{
    public record AddBreedCommand(string name, Guid id) : Abstractions.ICommand;
    
}