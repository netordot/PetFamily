namespace PetFamily.API.Contracts
{
    public record GetBreedsBySpeciesIdRequest(int Page, int PageSize, string? Sortby, string? OrderBy);
    
}
