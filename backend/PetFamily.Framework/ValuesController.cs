using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PetFamily.Core.Models;

namespace PetFamily.Framework
{
    [Route("api/[controller]")]
    [ApiController]

    public class ValuesController : ControllerBase
    {
        public override OkObjectResult Ok(object? value)
        {
            var envelope = Envelope.Ok(value);
            return base.Ok(envelope);
        }
    }
}
