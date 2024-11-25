using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using FluentValidation;
using PetFamily.Application.PetManagement.Queries.GetVolunteersWithPagination;
using CSharpFunctionalExtensions;
using PetFamily.Application.PetManagement.Queries.GetVolunteer;
using System.Security.Cryptography.X509Certificates;
using PetFamily.Application.PetManagement.Queries.GetPetsWithPagination;
using Microsoft.AspNetCore.Authorization;
using PetFamily.Core.Extensions;
using PetFamily.Application.PetManagement.Commands.Volunteers.AddPet;
using PetFamily.Application.PetManagement.Commands.Volunteers.Create;
using PetFamily.Application.PetManagement.Commands.Volunteers.SetPetMainPhoto;
using PetFamily.Application.PetManagement.Commands.Volunteers.UpdateMainInfo;
using PetFamily.Application.PetManagement.Commands.Volunteers.UpdatePetMainInfo;
using PetFamily.Application.PetManagement.Commands.Volunteers.UpdateRequisites;
using PetFamily.Application.PetManagement.Commands.Volunteers.UpdateSocials;
using PetFamily.Application.Volunteers.FullDeletePet;
using PetFamily.Framework;
using PetFamily.Volunteers.Presentation.Volunteers.Contracts;
using PetFamily.Volunteers.Application.Commands.Create;
using PetFamily.Volunteers.Application.Commands.UpdateMainInfo;
using PetFamily.Volunteers.Application.Commands.UpdateRequisites;
using PetFamily.Volunteers.Application.Commands.Delete;
using PetFamily.Volunteers.Application.Commands.AddPet.AddPhoto;
using PetFamily.Volunteers.Application.Querries.GetVolunteersWithPagination;
using PetFamily.Volunteers.Application.Querries.GetVolunteer;
using PetFamily.Volunteers.Application.Commands.AddNewPhotosToPet;
using PetFamily.Volunteers.Application.Commands.DeletePetPhoto;
using PetFamily.Volunteers.Application.Commands.SetPetMainPhoto;
using PetFamily.Volunteers.Application.Commands.SoftDeletePet;
using PetFamily.Volunteers.Application.Commands.FullDeletePet;
using PetFamily.Volunteers.Application.Commands.ChangePetStatus;
using PetFamily.Volunteers.Presentation.Pets.Contracts;
using PetFamily.Volunteers.Application.Querries.GetPet;
using PetFamily.Volunteers.Application.Commands.UpdateSocials;
using PetFamily.Volunteers.Presentation.Processors;
using PetFamily.Volunteers.Application.Commands.UpdatePetMainInfo;
using PetFamily.Framework.Authorization.Attributes;
using PetFamily.Framework.Authorization;

namespace PetFamily.API.Controllers;

[ApiController]
[Route("[controller]")]
public class VolunteersController : ValuesController
{
    [PermissionRequirement(Policies.PetManagement.Create)]
    [HttpPost]
    public async Task<ActionResult> Create(
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken,
        [FromServices] CreateVolunteerHandler createVolunteerService)
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

        var result = await createVolunteerService.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return new ObjectResult(result.Value) { StatusCode = 201 };
    }
    [PermissionRequirement(Policies.PetManagement.Update)]
    [HttpPatch("{id:guid}/main-info")]
    public async Task<ActionResult> UpdateMainInfo(
        [FromRoute] Guid id,
        [FromServices] UpdateVolunteerHandler service,
        [FromBody] UpdateVolunteerRequest request,
        [FromServices] IValidator<UpdateVolunteerCommand> validator,
        CancellationToken cancellationToken)
    {
        var command = new UpdateVolunteerCommand(request, id);

        // исправить, очень дурной тон запихивать реквест в комманду
        var result = await service.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return new ObjectResult(result.Value) { StatusCode = 200 };
    }

    [PermissionRequirement(Policies.PetManagement.Update)]
    [HttpPatch("{id:guid}/socials")]
    public async Task<ActionResult> UpdateSocials(
        [FromRoute] Guid id,
        [FromServices] UpdateSocialsHandler service,
        [FromBody] UpdateSocialsRequest request,
        [FromServices] IValidator<UpdateSocialsCommand> validator,
        CancellationToken cancellationToken)
    {
        var command = new UpdateSocialsCommand(id, request.socials);

        var result = await service.Handle
            (command, cancellationToken);

        return new ObjectResult(result.Value) { StatusCode = 200 };
    }

    [PermissionRequirement(Policies.PetManagement.Update)]
    [HttpPatch("{id:guid}/requisites")]
    public async Task<ActionResult> UpdateRequisites(
        [FromRoute] Guid id,
        [FromServices] UpdateRequisitesHandler service,
        [FromBody] UpdateRequisitesRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateRequisitesCommand(request.requisites, id);

        var result = await service.Handle(command, cancellationToken);

        return new ObjectResult(result.Value) { StatusCode = 200 };
    }

    [PermissionRequirement(Policies.PetManagement.Delete)]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(
       [FromRoute] Guid id,
       [FromServices] DeleteVolunteerHandler service,
       CancellationToken cancellationToken)
    {
        var request = new DeleteVolunteerCommand(id);

        var result = await service.Handle(request, cancellationToken);

        return new ObjectResult(result.Value) { StatusCode = 200 };
    }
    [PermissionRequirement(Policies.PetManagement.CreatePet)]
    [HttpPost("{id:guid}/pet")]
    public async Task<ActionResult> AddPet
        (
        [FromRoute] Guid id,
        [FromForm] AddPetRequest request,
        [FromServices] AddPetHandler addPetService,
        CancellationToken cancellationToken
        )
    {
        var command = new AddPetCommand(
            id,
            request.Name,
            request.SpeciesId,
            request.BreedId,
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

        var result = await addPetService.Handle(command, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return new ObjectResult(result.Value) { StatusCode = 201 };
    }

    [PermissionRequirement(Policies.PetManagement.UpdatePet)]
    [HttpPatch("{volunteerId:guid}/pets/{petId:guid}/photos")]
    public async Task<ActionResult> AddPhotosToPet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] AddPetFilesHandler service,
        [FromForm] AddFilesRequest request,
        CancellationToken cancellation
        )
    {
        await using var fileProcessor = new FormFileProcessor();

        var fileDtos = fileProcessor.Process(request.files);

        var addFilesCommand = new AddFileCommand(fileDtos);
        var result = await service.Handle(petId, volunteerId, addFilesCommand, cancellation);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return new ObjectResult(result.Value) { StatusCode = 200 };
    }

    [PermissionRequirement(Policies.PetManagement.Get)]
    [HttpPost("/getall")]
    public async Task<ActionResult> GetAllVolunteers(
        [FromServices] GetVolunteersWithPaginationHandler handler,
        [FromForm] GetVolunteersWithPaginationRequest request,
        CancellationToken cancellationToken)
    {

        var query = new GetVolunteersWithPaginationQuery(request.Page, request.PageSize, request.SortBy, request.SortDirection);
        var result = await handler.Handle(query, cancellationToken);

        return new ObjectResult(result) { StatusCode = 200 };
    }
    [PermissionRequirement(Policies.PetManagement.Get)]
    [HttpGet("{id:guid}/get")]
    public async Task<ActionResult> GetById(
        [FromRoute] Guid id,
        [FromServices] GetVolunteerHandler handler,
        CancellationToken cancellation)
    {
        var command = new GetVolunteerCommand(id);

        var result = await handler.Handle(command, cancellation);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }

        return new ObjectResult(result.Value) { StatusCode = 200 };

    }
    [PermissionRequirement(Policies.PetManagement.UpdatePet)]
    [HttpPut("{volunteerId:guid}/pets/{petId:guid}/update")]
    public async Task<ActionResult> UpdatePetInfo(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromForm] UpdatePetMainInfoRequest request,
        [FromServices] UpdatePetMainInfoHandler handler,
        CancellationToken cancellation)
    {
        var command = new UpdatePetMainInfoCommand(
            volunteerId,
            petId,
            request.Name,
            request.SpeciesId,
            request.BreedId,
            request.PhoneNumber,
            request.Color,
            request.Description,
            request.HealthCondition,
            request.City,
            request.Street,
            request.BuildingNumber,
            request.CorpsNumber,
            request.status,
            request.Weight,
            request.Height,
            request.IsCastrated,
            request.IsVaccinated,
            request.BirthDate,
            request.Requisites
            );

        var result = await handler.Handle(command, cancellation);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }

        return new ObjectResult(result.Value) { StatusCode = 200 };

    }
    [PermissionRequirement(Policies.PetManagement.UpdatePet)]
    [HttpPatch("{volunteerId:guid}/pets/{petId:guid}/photos/update")]
    public async Task<ActionResult> AddPetPhotos
        ([FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] AddNewPhotosToPetHandler handler,
        [FromForm] AddNewFilesToPetRequest request,
        CancellationToken cancellation)
    {
        await using var fileProcessor = new FormFileProcessor();

        var fileDtos = fileProcessor.Process(request.files);
        var newFilesCommand = new AddNewPetFilesCommand(fileDtos, petId, volunteerId);

        var result = await handler.Handle(newFilesCommand, cancellation);

        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }

        return new ObjectResult(result.Value) { StatusCode = 200 };
    }
    [PermissionRequirement(Policies.PetManagement.UpdatePet)]
    [HttpPatch("{volunteerId:guid}/pets/{petId:guid}/photos/delete")]
    public async Task<ActionResult> DeletePetPhoto
        ([FromForm] DeletePhotoRequest request,
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] DeletePetPhotoHandler handler,
        CancellationToken cancellation
        )
    {
        var command = new DeletePetPhotoCommand(request.Path, petId, volunteerId);

        var result = await handler.Handle(command, cancellation);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }

        return new ObjectResult(result.Value) { StatusCode = 200 };

    }

    [PermissionRequirement(Policies.PetManagement.UpdatePet)]
    [HttpPatch("{volunteerId:guid}/pets/{petId:guid}/photos/main")]
    public async Task<ActionResult> SetMainPhoto(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] SetPetMainPhotoHandler handler,
        [FromForm] SetPetMainPhotoRequest request,
        CancellationToken cancellation)
    {
        var command = new SetPetMainPhotoCommand(volunteerId, petId, request.Path);

        var result = await handler.Handle(command, cancellation);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }

        return new ObjectResult(result.Value) { StatusCode = 200 };
    }

    [PermissionRequirement(Policies.PetManagement.DeletePet)]
    [HttpPatch("{volunteerId:guid}/pets/{petId:guid}/softdelete")]
    public async Task<ActionResult> SoftDeletePet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] SoftDeletePetHandler handler,
        CancellationToken cancellation)
    {
        var command = new SoftDeletePetCommand(volunteerId, petId);

        var result = await handler.Handle(command, cancellation);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }

        return new ObjectResult(result.Value) { StatusCode = 200 };
    }

    [PermissionRequirement(Policies.PetManagement.DeletePet)]
    [HttpPatch("{volunteerId:guid}/pets/{petId:guid}/fulldelete")]
    public async Task<ActionResult> FullDeletePet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] FullDeletePetHandler handler,
        CancellationToken cancellation)
    {
        var command = new FullDeletePetCommand(volunteerId, petId);
        var result = await handler.Handle(command, cancellation);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }

        return new ObjectResult(result.Value) { StatusCode = 200 };
    }
    [PermissionRequirement(Policies.PetManagement.UpdatePet)]
    [HttpPatch("{volunteerId:guid}/pets/{petId:guid}/status")]
    public async Task<ActionResult> ChangePetStaus(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] ChangePetStatusHandler handler,
        [FromForm] ChangePetStatusRequest request,
        CancellationToken cancellation)
    {
        var command = new ChangePetStatusCommand(request.Status, volunteerId, petId);
        var result = await handler.Handle(command, cancellation);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }

        return new ObjectResult(result.Value) { StatusCode = 200 };

    }

}