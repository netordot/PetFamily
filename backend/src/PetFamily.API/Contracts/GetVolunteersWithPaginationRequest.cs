using PetFamily.Application.PetManagement.Queries.GetVolunteersWithPagination;

namespace PetFamily.API.Contracts
{
    public record GetVolunteersWithPaginationRequest
    (
        int Page,
        int PageSize,
        string? SortBy,
        string? SortDirection

        //public GetVolunteersWithPaginationQuery ToQuery()
        //{
        //    return new GetVolunteersWithPaginationQuery(Page, PageSize, SortBy, SortDirection);
        //}
    );
}
