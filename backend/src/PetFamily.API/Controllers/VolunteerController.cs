using Microsoft.AspNetCore.Mvc;
using PetFamily.Application.Volunteers.CreateVolunteer;
using System.Reflection.Metadata.Ecma335;
using PetFamily.API.Extensions;

namespace PetFamily.API.Controllers;

[ApiController]
[Route("[controller]")]
public class VolunteerController : ControllerBase
{
    private readonly ICreateVolunteerService _createVolunteerService;

    public VolunteerController(ICreateVolunteerService createVolunteerService)
    {
        _createVolunteerService = createVolunteerService;
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> Create
        ([FromBody] CreateVolunteerRequest createVolunteerRequest,CancellationToken cancellationToken)
    {
        var result =await _createVolunteerService.Create(createVolunteerRequest, cancellationToken);
        if (result.IsFailure)
        {
            return result.Error.ToResponse();
        }

        return Created(result.Value.ToString(), null);
    }

}