﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetFamily.Accounts.Presentation.Contracts;
using PetFamily.Application.AccountManagement.Commands;
using PetFamily.Application.AccountManagement.Commands.LogIn;
using PetFamily.Framework;
using PetFamily.Core.Extensions;

namespace PetFamily.Accounts.Presentation.Controllers.Authorization
{
    [Route("[controller]")]
    [ApiController]
    public class AuthorizationController : ValuesController
    {
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

            return new ObjectResult(result.Value) { StatusCode = 200 };
        }
    }
}