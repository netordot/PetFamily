using Microsoft.AspNetCore.Mvc;
using PetFamily.Framework;
using PetFamily.Volunteers.Application.Querries.GetPet;
using PetFamily.Volunteers.Presentation.Pets.Contracts;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetFamily.Volunteers.Application.Querries.GetPetsWithPagination;
using PetFamily.SharedKernel.Other;
using PetFamily.Core.Extensions;
using PetFamily.Framework.Authorization;
using PetFamily.Framework.Authorization.Attributes;

namespace PetFamily.Volunteers.Presentation.Pets
{
    [ApiController]
    [Route("[controller]")]
    public class PetsController : ValuesController
    {
        [PermissionRequirement(Policies.PetManagement.Get)]
        [HttpPost("pets/")]
        public async Task<ActionResult> GetAllPetsWithPagination
       (
       [FromServices] GetPetsWithPaginationHandler handler,
       [FromForm] GetPetsWithPaginationRequest request,
       CancellationToken cancellationToken
       )
        {
            var query = request.ToQuery();
            var result = await handler.Handle(query, cancellationToken);

            return new ObjectResult(result) { StatusCode = 200 };
        }
        [PermissionRequirement(Policies.PetManagement.GetPet)]
        [HttpPost("pets/{petId:guid}")]
        public async Task<ActionResult> GetPetById(
            [FromRoute] Guid petId,
            [FromServices] GetPetHandler handler,
            CancellationToken cancellation
            )
        {
            var query = new GetPetQuery(petId);
            var result = await handler.Handle(query, cancellation);

            if (result == null)
            {
                return Errors.General.NotFound(petId).ToResponse();
            }

            return new ObjectResult(result) { StatusCode = 200 };
        }
    }
}
