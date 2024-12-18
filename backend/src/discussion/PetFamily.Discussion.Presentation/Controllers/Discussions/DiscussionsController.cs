using Microsoft.AspNetCore.Mvc;
using PetFamily.Discussion.Application.Commands.CreateDiscussion;
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

            var result = handler.Handle(command, cancellation);

            return new ObjectResult(result) { StatusCode = 200 };

        }
    }
    
}
