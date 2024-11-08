namespace PetFamily.Application.Species.DeleteBreed
{
    public record DeleteBreedCommand(Guid Speciesid, Guid BreedId) : Abstractions.ICommand;

}
