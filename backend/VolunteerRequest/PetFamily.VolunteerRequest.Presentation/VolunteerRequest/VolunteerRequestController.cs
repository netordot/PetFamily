using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;
using PetFamily.Core.Dtos.VolunteerRequest;
using PetFamily.Core.Extensions;
using PetFamily.Framework;
using PetFamily.Framework.Authorization;
using PetFamily.Framework.Authorization.Attributes;
using PetFamily.VolunteerRequest.Application.Commands.ApproveVolunteerRequest;
using PetFamily.VolunteerRequest.Application.Commands.CreateVolunteerRequest;
using PetFamily.VolunteerRequest.Application.Commands.DeclineRequest;
using PetFamily.VolunteerRequest.Application.Commands.SetRequestForRemake;
using PetFamily.VolunteerRequest.Application.Commands.UpdateVolunteerRequest;
using PetFamily.VolunteerRequest.Application.Queries.GetUnsubmittedVolunteerRequestsWithPagination;
using PetFamily.VolunteerRequest.Application.Queries.GetVolunteerRequestsForAdmin;
using PetFamily.VolunteerRequest.Application.Queries.GetVolunteerRequestsForParticipant;
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
        [PermissionRequirement(Policies.VolunteerRequest.Create)]
        [HttpPost("user/{id:guid}/application")]
        public async Task<ActionResult> CreateVolunteerRequest(
            [FromServices] CreateVolunteerRequestHandler handler,
            [FromBody] CreateVolunteerRequestRequest request,
            [FromRoute] Guid id,
            CancellationToken cancellation)
        {
            var command = request.ToCommand(id);

            var result = await handler.Handle(command, cancellation);
            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return new ObjectResult(result.IsSuccess) { StatusCode = 200 };
        }

        [PermissionRequirement(Policies.VolunteerRequest.Approve)]
        [HttpPatch("/admin/{adminId:guid}/application/{volunteerRequestId:guid}/approval")]
        public async Task<ActionResult> ApproveVolunteerRequest(
            [FromRoute] Guid volunteerRequestId,
            [FromRoute] Guid adminId,
            [FromServices] ApproveVolunteerRequestHandler handler,
            CancellationToken cancellation)

        {
            var command = new ApproveVolunteerRequestCommand(adminId, volunteerRequestId);

            var result = await handler.Handle(command, cancellation);
            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return new ObjectResult(null) { StatusCode = 200 };
        }

        [PermissionRequirement(Policies.VolunteerRequest.Decline)]
        [HttpPatch("/admin/{adminId:guid}/application/{requestId:guid}/rejection")]
        public async Task<ActionResult> DeclineVolunteerRequest(
            [FromRoute] Guid adminId,
            [FromRoute] Guid requestId,
            [FromBody] DeclineRemakeApplicationRequest request,
            [FromServices] DeclineRequestHandler handler,
            CancellationToken cancellation
            )
        {
            var command = request.ToDeclineCommand(adminId, requestId);

            var result = await handler.Handle(command, cancellation);
            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return new ObjectResult(null) { StatusCode = 200 };
        }

        [PermissionRequirement(Policies.VolunteerRequest.SendToRemake)]
        [HttpPatch("/admin/{adminId:guid}/application/{volunteerRequestId:guid}/remake")]
        public async Task<ActionResult> SetRequestForRemake(
           [FromRoute] Guid adminId,
           [FromRoute] Guid requestId,
           [FromBody] DeclineRemakeApplicationRequest request,
           [FromServices] SetRequestForRemakeHandler handler,
           CancellationToken cancellation
           )
        {
            var command = request.ToRemakeCommand(adminId, requestId);

            var result = await handler.Handle(command, cancellation);
            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return new ObjectResult(null) { StatusCode = 200 };
        }

        [PermissionRequirement(Policies.VolunteerRequest.Update)]
        [HttpPatch("user/{userId:guid}/application/{requestId:guid}/update")]
        public async Task<ActionResult> UpdateVolunteerRequest(
         [FromRoute] Guid userId,
         [FromRoute] Guid requestId,
         [FromBody] VolunteerInfoDto request,
         [FromServices] UpdateVolunteerRequestHandler handler,
         CancellationToken cancellation
         )
        {
            var command = new UpdateVolunteerRequestCommand(userId, requestId, request);

            var result = await handler.Handle(command, cancellation);
            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return new ObjectResult(null) { StatusCode = 200 };
        }

        [PermissionRequirement(Policies.VolunteerRequest.Get)]
        [HttpGet]
        public async Task<ActionResult> GetUnmoderatedRequests(
            [FromForm] GetWithPagiantionRequest request,
            [FromServices] GetUnsubmittedVolunteerRequestsWithPaginationHandler handler,
            CancellationToken cancellation
           )
        {
            var query = new GetUnsubmittedVolunteerRequestsWithPaginationQuery(request.Page, request.PageSize);

            var result = await handler.Handle(query, cancellation);

            return new ObjectResult(result) { StatusCode = 200 };

        }


        [PermissionRequirement(Policies.VolunteerRequest.GetForAdmin)]
        [HttpGet("admin/{adminId:guid}/applications")]
        public async Task<ActionResult> GetAdminRequests(
           [FromForm] GetWithPaginationForAdminRequest request,
           [FromRoute] Guid adminId,
           [FromServices] GetVolunteerRequestsForAdminHandler handler,
           CancellationToken cancellation
          )
        {
            var query = new GetVolunteerRequestsForAdminQuery(request.Page, request.PageSize, adminId, request.Status);

            var result = await handler.Handle(query, cancellation);

            return new ObjectResult(result) { StatusCode = 200 };
        }

        [PermissionRequirement(Policies.VolunteerRequest.GetForUser)]
        [HttpGet("user/{userId:guid}/applications")]
        public async Task<ActionResult> GetUserRequests(
          [FromForm] GetWithPaginationForUserRequest request,
          [FromRoute] Guid userId,
          [FromServices] GetVolunteerRequestsForParticipantHandler handler,
          CancellationToken cancellation
         )
        {
            var query = request.ToQuery(userId);

            var result = await handler.Handle(query, cancellation);

            return new ObjectResult(result) { StatusCode = 200 };
        }


    }
}
