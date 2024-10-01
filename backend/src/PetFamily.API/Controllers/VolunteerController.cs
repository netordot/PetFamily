using Microsoft.AspNetCore.Mvc;
using PetFamily.Application.Volunteers.CreateVolunteer;
using System.Reflection.Metadata.Ecma335;
using FluentValidation;
using PetFamily.API.Extensions;
using PetFamily.API.Response;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.API.Controllers;

[ApiController]
[Route("[controller]")]
public class VolunteerController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Create
    ([FromBody] CreateVolunteerRequest createVolunteerRequest, CancellationToken cancellationToken,
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
}