namespace PetFamily.Species.Presentation.Contracts
{
    public record GetAllSpeciesWithPaginationRequest(string SortOrder, int Page, int PageSize);

}
