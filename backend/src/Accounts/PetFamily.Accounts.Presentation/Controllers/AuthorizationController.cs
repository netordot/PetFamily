using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Accounts.Presentation.Contracts;
using PetFamily.Application.AccountManagement.Commands.LogIn;
using PetFamily.Framework;
using PetFamily.Core.Extensions;
using PetFamily.Accounts.Application.Commands.Register;
using PetFamily.Accounts.Application.Commands.Refresh;

namespace PetFamily.Accounts.Presentation.Controllers.Authorization
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorizationController : ValuesController
    {
        private const string REFRESH_TOKEN = "refresh_token";

        [HttpPost("registration")]
        public async Task<ActionResult> Register( 
            [FromBody] RegisterUserRequest request,
            [FromServices] RegisterUserHandler handler,
            CancellationToken cancellation
            )
        {
            var command = new RegisterUserCommand(request.Email, request.UserName, request.Password);
            var result = await handler.Handle(command, cancellation);
            if(result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(
            [FromBody] LogInUserRequest request,
            [FromServices] LogInUserHandler handler,
            CancellationToken cancellation
            )
        {
            var command = new LogInUserCommand(request.Email, request.Password);
            var result = await handler.Handle(command, cancellation);
            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }
            HttpContext.Response.Cookies.Append(REFRESH_TOKEN, result.Value.RefreshToken.ToString());

            return new ObjectResult(result.Value) { StatusCode = 200 };
        }

        [HttpPost("refresh")]
        public async Task<ActionResult> Refresh(
            [FromServices] RefreshHandler handler,
            CancellationToken cancellation )
        {
            if(HttpContext.Request.Cookies.TryGetValue(REFRESH_TOKEN, out var refreshToken) == false)
            {
                return Unauthorized();
            }

            var command = new RefreshCommand(Guid.Parse(refreshToken));

            var result = await handler.Handle(command, cancellation);
            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            HttpContext.Response.Cookies.Append(REFRESH_TOKEN, result.Value.RefreshToken.ToString());

            return new ObjectResult(result.Value) { StatusCode = 200 };   
        }
    }
}
