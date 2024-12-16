using Microsoft.AspNetCore.Mvc;
using PetFamily.Core.Extensions;
using PetFamily.Framework;
using PetFamily.VolunteerRequest.Application.Commands.CreateVolunteerRequest;
using PetFamily.VolunteerRequest.Presentation.VolunteerRequest.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Presentation.VolunteerRequest
{
    [Route("[controller]")]
    [ApiController]
    public class VolunteerRequestController : ValuesController
    {
        [HttpPost("{id:guid}/volunteerrequest")]
        public async Task<ActionResult> CreateVolunteerRequest(
            [FromServices] CreateVolunteerRequestHandler handler,
            [FromBody] CreateVolunteerRequestRequest request,
            [FromRoute] Guid id,
            CancellationToken cancellation)
        {
            var command = request.ToCommand(id);

            var result = await handler.Handle(command, cancellation);
            if(result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return new ObjectResult(result.IsSuccess) { StatusCode = 200 };
        }
    }
}
