using PetFamily.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.PetManagement.Queries.GetPetsWithPagination.GetPetsWithPagination
{
    public record GetPetsWithPaginationQuery(
    int Page,
    int PageSize,
    string? SortBy,
    string? SortDirection,
    Guid? VolunteerId,
    string? NickName,
    Guid? SpeciesId,
    Guid? BreedId,
    string? Street,
    string? City,
    int? BuildingNumber,
    int? CorpsNumber,
    int? MinHeight,
    int? MaxHeight,
    int? MinWeight,
    int? MaxWeight,
    int? MinAge,
    int? MaxAge,
    int? HelpStatus,
    bool? IsCastrated,
    bool? IsVaccinated) : IQuery;


}
