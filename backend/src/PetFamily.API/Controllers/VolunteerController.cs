using Microsoft.AspNetCore.Mvc;
using PetFamily.Application.Volunteers.CreateVolunteer;
using System.Reflection.Metadata.Ecma335;
using FluentValidation;
using PetFamily.API.Extensions;
using PetFamily.API.Response;
using PetFamily.Application.Volunteers.SharedDtos;
using PetFamily.Application.Volunteers.UpdateRequisites;
using PetFamily.Application.Volunteers.UpdateSocials;
using PetFamily.Application.Volunteers.UpdateVolunteer;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Application.Volunteers.Delete;
using PetFamily.API.Contracts;
using PetFamily.Application.Volunteers.AddPet;

namespace PetFamily.API.Controllers;

[ApiController]
[Route("[controller]")]
public class VolunteerController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Create(
        [FromBody] CreateVolunteerRequest createVolunteerRequest,
        CancellationToken cancellationToken,
        [FromServices] ICreateVolunteerService createVolunteerService,
        [FromServices] IValidator<CreateVolunteerRequest> validator)
    {
        // тут обернуть блок try catch, его обрабатывает middleware


        var validationResult = await validator.ValidateAsync(createVolunteerRequest, cancellationToken);

        if (!validationResult.IsValid)
        {
            return validationResult.ToValidationErrorResponse();
        }

        var result = await createVolunteerService.Create(createVolunteerRequest, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return new ObjectResult(result.Value) { StatusCode = 201 };
    }

    [HttpPatch("{id:guid}/main-info")]
    public async Task<ActionResult> UpdateMainInfo(
        [FromRoute] Guid id,
        [FromServices] IUpdateVolunteerService service,
        [FromBody] UpdateVolunteerDto Dto,
        [FromServices] IValidator<UpdateVolunteerRequest> validator,
        CancellationToken cancellationToken)
    {
        var request = new UpdateVolunteerRequest(Dto, id);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToValidationErrorResponse();

        var result = await service.Update(request, cancellationToken);

        return new ObjectResult(result.Value) { StatusCode = 200 };
    }

    [HttpPatch("{id:guid}/socials")]
    public async Task<ActionResult> UpdateSocials(
        [FromRoute] Guid id,
        [FromServices] IUpdateSocialsService service,
        [FromBody] SocialsListDto Dto,
        [FromServices] IValidator<UpdateSocialsRequest> validator,
        CancellationToken cancellationToken)
    {
        var request = new UpdateSocialsRequest(id, Dto);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToValidationErrorResponse();

        var result = await service.UpdateSocials
            (request, cancellationToken);

        return new ObjectResult(result.Value) { StatusCode = 200 };
    }

    [HttpPatch("{id:guid}/requisites")]
    public async Task<ActionResult> UpdateRequisites(
        [FromRoute] Guid id,
        [FromServices] IUpdateRequisitesService service,
        [FromBody] RequisiteListDto Dto,
        [FromServices] IValidator<UpdateRequisitesRequest> validator,
        CancellationToken cancellationToken)
    {
        var request = new UpdateRequisitesRequest(Dto, id);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToValidationErrorResponse();

        var result = await service.UdpateRequisites(request, cancellationToken);

        return new ObjectResult(result.Value) { StatusCode = 200 };
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(
       [FromRoute] Guid id,
       [FromServices] IDeleteVolunteerService service,
       [FromServices] IValidator<DeleteVolunteerRequest> validator,
       CancellationToken cancellationToken)
    {
        var request = new DeleteVolunteerRequest(id);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToValidationErrorResponse();

        var result = await service.Delete(request, cancellationToken);

        return new ObjectResult(result.Value) { StatusCode = 200 };
    }

    [HttpPost("{id:guid}/pet")]
    public async Task<ActionResult> AddPet
        (
        [FromRoute] Guid id,
        [FromBody] AddPetRequest request,
        [FromServices] AddPetService addPetService,
        CancellationToken cancellationToken
        )
    {
        var command = new AddPetCommand(
            id,
            request.Name,
            request.Species,
            request.Breed,
            request.Color,
            request.Description,
            request.HealthCondition,
            request.status,
            request.Weight,
            request.Height,
            request.IsCastrated,
            request.IsVaccinated,
            request.BirthDate
            );

        var result = await addPetService.AddPet(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return new ObjectResult(result.Value) { StatusCode = 201 };

    }


}