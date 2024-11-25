using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Application.Species;
using PetFamily.Application.Species.AddBreeds;
using PetFamily.Application.Species.CreateSpecies;
using PetFamily.Application.Species.DeleteBreed;
using PetFamily.Core.Extensions;
using PetFamily.Framework.Authorization;
using PetFamily.Framework.Authorization.Attributes;
using PetFamily.SharedKernel.Id;
using PetFamily.Species.Application.Commands.AddBreeds;
using PetFamily.Species.Application.Commands.CreateSpecies;
using PetFamily.Species.Application.Commands.DeleteBreed;
using PetFamily.Species.Application.Commands.DeleteSpecies;
using PetFamily.Species.Application.Querries.GetAllSpecies;
using PetFamily.Species.Application.Querries.GetBreedsWithPagination;
using PetFamily.Species.Presentation.Contracts;

namespace PetFamily.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        [PermissionRequirement(Policies.SpeciesManagement.Create)]
        [HttpPost]
        public async Task<ActionResult> Create
            ([FromServices] CreateSpeciesHandler service,
            [FromBody] CreateSpeciesRequest request,
            CancellationToken cancellationToken
            )
        {
            var speciesId = SpeciesId.NewSpeciesId;

            var speciesCommand = new CreateSpeciesCommand(request.speciesName, speciesId);

            var result = await service.Handle(speciesCommand, cancellationToken);
            if (result.IsFailure)
                return result.Error.ToResponse();

            return new ObjectResult(result.Value) { StatusCode = 201 };
        }

        [PermissionRequirement(Policies.SpeciesManagement.CreateBreed)]
        [HttpPatch("{id:guid}/breed")]
        public async Task<ActionResult> AddBreeds
            ([FromServices] AddBreedHandler addBreedService,
             [FromRoute] Guid id,
             [FromBody] AddBreedRequest request,
             CancellationToken cancellationToken
            )
        {
            var command = new AddBreedCommand(request.breedName, id);

            var result = await addBreedService.Handle(command, cancellationToken);
            if (result.IsFailure)
                return result.Error.ToResponse();

            return new ObjectResult(result.Value) { StatusCode = 201 };
        }
        [PermissionRequirement(Policies.SpeciesManagement.Delete)]
        [HttpDelete("{id:guid}/delete")]
        public async Task<ActionResult> DeleteSpecies(
            [FromServices] DeleteSpeciesHandler handler,
            [FromRoute] Guid id,
            CancellationToken cancellation)
        {
            var command = new DeleteSpeciesCommand(id);

            var result = await handler.Handle(command, cancellation);
            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return new ObjectResult(result.Value) { StatusCode = 200 };
        }
        [PermissionRequirement(Policies.SpeciesManagement.DeleteBreed)]
        [HttpPatch("{speciesId:guid}/breeds/{breedId:guid}/delete")]
        public async Task<ActionResult> DeleteBreedById(
            [FromServices] DeleteBreedHandler handler,
            [FromRoute] Guid speciesId,
            [FromRoute] Guid breedId,
            CancellationToken cancellation)
        {
            var command = new DeleteBreedCommand(speciesId, breedId);

            var result = await handler.Handle(command, cancellation);
            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return new ObjectResult(result.Value) { StatusCode = 200 };
        }

        [PermissionRequirement(Policies.SpeciesManagement.Get)]
        [HttpPost("/species/getall")]
        public async Task<ActionResult> GetAllSpecies(
            [FromServices] GetAllSpeciesHandler handler,
            [FromForm] GetAllSpeciesWithPaginationRequest request,
            CancellationToken cancellation)
        {
            var query = new GetAllSpeciesWithPaginationQuery(request.Page, request.PageSize, request.SortOrder);
            var result = await handler.Handle(query, cancellation);
            return new ObjectResult(result) { StatusCode =200};
        }
        [PermissionRequirement(Policies.SpeciesManagement.GetBreed)]
        [HttpPost("{id:guid}/breeds")]
        public async Task<ActionResult> GetBreedsBySpeciesId(
            [FromServices] GetBreedsWithPaginationHanlder handler,
            [FromForm] GetBreedsBySpeciesIdRequest request,
            [FromRoute] Guid id,
            CancellationToken cancellation)
        {
            var query = new GetBreedsWithPaginationQuery(
                id, 
                request.Page, 
                request.PageSize, 
                request.OrderBy, 
                request.Sortby);

            var result = await handler.Handle(query, cancellation);

            return new ObjectResult(result) { StatusCode = 200 };
        }
    }

}
