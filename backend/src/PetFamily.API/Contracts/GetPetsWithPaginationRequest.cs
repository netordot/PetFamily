using Microsoft.AspNetCore.Mvc.RazorPages;
using PetFamily.Domain.Pet.Breed;
using PetFamily.Domain.Pet.Species;
using PetFamily.Domain.Volunteer;
using static System.Net.Mime.MediaTypeNames;
using System.Globalization;
using System.Reflection.Emit;
using PetFamily.Application.PetManagement.Queries.GetPetsWithPagination.GetPetsWithPagination;

namespace PetFamily.API.Contracts
{
    public record GetPetsWithPaginationRequest(
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
    bool? IsVaccinated)
    {
        public GetPetsWithPaginationQuery ToQuery() =>
        new(
                Page,
                PageSize,
                SortBy,
                SortDirection,
                VolunteerId,
                NickName,
                SpeciesId,
                BreedId,
                Street,
                City,
                BuildingNumber,
                CorpsNumber,
                MinHeight,
                MaxHeight,
                MinWeight,
                MaxWeight,
                MinAge,
                MaxAge,
                HelpStatus,
                IsCastrated,
                IsVaccinated);
    }
}
