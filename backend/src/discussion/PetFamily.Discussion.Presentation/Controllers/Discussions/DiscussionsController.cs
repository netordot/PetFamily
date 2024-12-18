using Microsoft.AspNetCore.Mvc;
using PetFamily.Core.Extensions;
using PetFamily.Discussion.Application.Commands.CloseDiscussion;
using PetFamily.Discussion.Application.Commands.CreateDiscussion;
using PetFamily.Discussion.Application.Commands.EditMessage;
using PetFamily.Discussion.Application.Commands.SendMessage;
using PetFamily.Discussion.Application.Queries.GetDiscussionWithMessages;
using PetFamily.Discussion.Presentation.Controllers.Contracts;
using PetFamily.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Presentation.Controllers.Discussions
{
    [ApiController]
    [Route("[controller]")]
    public class DiscussionsController : ValuesController
    {

        [HttpPost("discussion/")]
        public async Task<ActionResult> Create(
            [FromForm] CreateDiscussionRequest request,
            [FromServices] CreateDiscussionHandler handler,
            CancellationToken cancellation
            )
        {
            // TODO: реализовать ошибки, метод Create, а в следствии хендлер их не возвращают
            var command = request.ToCommand();

            var result = await handler.Handle(command, cancellation);

            return new ObjectResult(result) { StatusCode = 200 };

        }

        [HttpPatch("discussion/{id:guid}/closure")]
        public async Task<ActionResult> Close(
            [FromRoute] Guid id,
            [FromServices] CloseDiscussionHandler handler,
            [FromForm] CloseDiscussionRequest request,
            CancellationToken cancellation)
        {
            var command = request.ToCommand(id);

            var result = await handler.Handle(command, cancellation);
            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return new ObjectResult(result) { StatusCode = 200 };
        }

        [HttpPatch("discussion/{id:guid}/user{userId:guid}/message")]
        public async Task<ActionResult> SendMessage(
           [FromRoute] Guid id,
           [FromRoute] Guid userId,
           [FromServices] SendMessageHandler handler,
           [FromForm] SendMessageRequest request,
           CancellationToken cancellation)
        {
            var command = request.ToCommand(userId, id);

            var result = await handler.Handle(command, cancellation);
            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return new ObjectResult(result) { StatusCode = 200 };
        }

        [HttpPatch("discussion/{id:guid}/user{userId:guid}/message/{messageId:guid}/edit")]
        public async Task<ActionResult> EditMessage(
           [FromRoute] Guid id,
           [FromRoute] Guid userId,
           [FromRoute] Guid messageId,
           [FromServices] EditMessageHandler handler,
           [FromForm] EditMessageRequest request,
           CancellationToken cancellation)
        {
            var command = request.ToCommand(id, userId, messageId);

            var result = await handler.Handle(command, cancellation);
            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return new ObjectResult(result) { StatusCode = 200 };
        }

        [HttpPatch("discussion/{id:guid}/user{userId:guid}/message/{messageId:guid}/delete")]
        public async Task<ActionResult> DeleteMessage(
           [FromRoute] Guid id,
           [FromRoute] Guid userId,
           [FromRoute] Guid messageId,
           [FromServices] EditMessageHandler handler,
           [FromForm] EditMessageRequest request,
           CancellationToken cancellation)
        {
            var command = request.ToCommand(id, userId, messageId);

            var result = await handler.Handle(command, cancellation);
            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return new ObjectResult(result) { StatusCode = 200 };
        }

        [HttpGet("discussion/{id:guid}/user/{userId:guid}/messages")]
        public async Task<ActionResult> GetMessages(
            [FromRoute] Guid id,
            [FromRoute] Guid userId,
            [FromServices] GetDiscussionWithMessagesHandler handler,
            CancellationToken cancellation)
        {
            var query = new GetDiscussionWithMessagesQuery(id, userId);

            var result = await handler.Handle(query, cancellation);

            return new ObjectResult(result) { StatusCode = 200 };
        }

    }

}
