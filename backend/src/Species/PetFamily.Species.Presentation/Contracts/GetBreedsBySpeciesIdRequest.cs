namespace PetFamily.Species.Presentation.Contracts
{
    public record GetBreedsBySpeciesIdRequest(int Page, int PageSize, string? Sortby, string? OrderBy);

}
