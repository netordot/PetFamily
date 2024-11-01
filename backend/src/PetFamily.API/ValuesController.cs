using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PetFamily.API.Response;

namespace PetFamily.API
{
    [Route("api/[controller]")]
    [ApiController]

    public class ValuesController : ControllerBase
    {
        public override OkObjectResult Ok( object? value)
        {
            var envelope = Envelope.Ok( value );
            return base.Ok(envelope);
        }
    }
}
