namespace PetFamily.API.Contracts
{
    public record GetAllSpeciesWithPaginationRequest(string SortOrder, int Page, int PageSize);
    
}
