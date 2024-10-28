using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.Application.Species;
using PetFamily.Application.Species.AddBreeds;
using PetFamily.Application.Species.CreateSpecies;
using PetFamily.Domain;
using PetFamily.Domain.Pet.Species;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Create
            ([FromServices] CreateSpeciesService service,
            [FromBody] CreateSpeciesRequest request,
            CancellationToken cancellationToken
            )
        {
            var speciesId = SpeciesId.NewSpeciesId;

            var speciesCommand = new CreateSpeciesCommand(request.speciesName, speciesId);

            var result =  await service.Create(speciesCommand,cancellationToken);
            if (result.IsFailure)
                return result.Error.ToResponse();

             return new ObjectResult(result.Value) { StatusCode=201};
        }

        [HttpPatch("{id:guid}/breed")]
        public async Task<ActionResult> AddBreeds
            ([FromServices] AddBreedService addBreedService,
             [FromRoute] Guid id,
             [FromBody] AddBreedRequest request,
             CancellationToken cancellationToken
            )
        {
            var command = new AddBreedCommand(request.breedName, id);

           var result = await addBreedService.AddBreed(command,cancellationToken);
            if(result.IsFailure)
                return result.Error.ToResponse();

            return new ObjectResult(result.Value) { StatusCode = 201 };
        }
    }
}
