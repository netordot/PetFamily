using PetFamily.Core.Abstractions;

namespace PetFamily.Species.Application.Commands.DeleteBreed
{
    public record DeleteBreedCommand(Guid Speciesid, Guid BreedId) : ICommand;

}
