using Microsoft.AspNetCore.Mvc;
using PetFamily.Application.Volunteers.CreateVolunteer;
using System.Reflection.Metadata.Ecma335;
using FluentValidation;
using PetFamily.API.Extensions;
using PetFamily.API.Response;
using PetFamily.Application.Volunteers.UpdateVolunteer;
using PetFamily.Domain.Shared.Errors;

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
}