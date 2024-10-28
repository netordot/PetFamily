using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using FluentValidation;
using PetFamily.API.Extensions;
using PetFamily.API.Response;
using PetFamily.Application.Volunteers.SharedDtos;
using PetFamily.Application.Volunteers.UpdateRequisites;
using PetFamily.Application.Volunteers.UpdateSocials;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Application.Volunteers.Delete;
using PetFamily.API.Contracts;
using PetFamily.Application.Volunteers.AddPet;
using PetFamily.Application.Volunteers.AddPet.AddPhoto;
using PetFamily.Application.Volunteers.Create;
using PetFamily.Application.Volunteers.UpdateMainInfo;
using PetFamily.API.Processors;

namespace PetFamily.API.Controllers;

[ApiController]
[Route("[controller]")]
public class VolunteerController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Create(
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken,
        [FromServices] ICreateVolunteerService createVolunteerService)
    {
        var command = new CreateVolunteerCommand(
            request.FirstName,
            request.MiddleName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            request.Experience,
            request.Description,
            request.City,
            request.Street,
            request.BuildingNumber,
            request.CorpsNumber,
            request.Requisites,
            request.SocialNetworks
            );

        var result = await createVolunteerService.Create(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return new ObjectResult(result.Value) { StatusCode = 201 };
    }

    [HttpPatch("{id:guid}/main-info")]
    public async Task<ActionResult> UpdateMainInfo(
        [FromRoute] Guid id,
        [FromServices] UpdateVolunteerService service,
        [FromBody] UpdateVolunteerRequest request,
        [FromServices] IValidator<UpdateVolunteerCommand> validator,
        CancellationToken cancellationToken)
    {
        var command = new UpdateVolunteerCommand(request, id);

        var result = await service.Update(command, cancellationToken);
        if(result.IsFailure)
            return result.Error.ToResponse();

        return new ObjectResult(result.Value) { StatusCode = 200 };
    }

    [HttpPatch("{id:guid}/socials")]
    public async Task<ActionResult> UpdateSocials(
        [FromRoute] Guid id,
        [FromServices] UpdateSocialsService service,
        [FromBody] UpdateSocialsRequest request,
        [FromServices] IValidator<UpdateSocialsCommand> validator,
        CancellationToken cancellationToken)
    {
        var command = new UpdateSocialsCommand(id, request.socials);

        //var validationResult = await validator.ValidateAsync(request, cancellationToken);
        //if (!validationResult.IsValid)
        //    return validationResult.ToValidationErrorResponse();

        var result = await service.UpdateSocials
            (command, cancellationToken);

        return new ObjectResult(result.Value) { StatusCode = 200 };
    }

    [HttpPatch("{id:guid}/requisites")]
    public async Task<ActionResult> UpdateRequisites(
        [FromRoute] Guid id,
        [FromServices] UpdateRequisitesService service,
        [FromBody] UpdateRequisitesRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateRequisitesCommand(request.requisites, id);

        var result = await service.UdpateRequisites(command, cancellationToken);

        return new ObjectResult(result.Value) { StatusCode = 200 };
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(
       [FromRoute] Guid id,
       [FromServices] DeleteVolunteerService service,
       CancellationToken cancellationToken)
    {
        var request = new DeleteVolunteerCommand(id);

        var result = await service.Delete(request, cancellationToken);

        return new ObjectResult(result.Value) { StatusCode = 200 };
    }

    [HttpPost("{id:guid}/pet")]
    public async Task<ActionResult> AddPet
        (
        [FromRoute] Guid id,
        [FromForm] AddPetRequest request,
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

    [HttpPatch("{volunteerId:guid}/pets/{petId:guid}/photos")]
    public async Task<ActionResult> AddPhotosToPet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] AddPetFilesService service,
        [FromForm] AddFilesRequest request,
        CancellationToken cancellation
        )
    {
        await using var fileProcessor = new FormFileProcessor();

        var fileDtos = fileProcessor.Process(request.files);

        var addFilesCommand = new AddFileCommand(fileDtos);
        var result = await service.AddPetFiles(petId, volunteerId, addFilesCommand, cancellation);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return new ObjectResult(result.Value) { StatusCode = 200};
    }
}