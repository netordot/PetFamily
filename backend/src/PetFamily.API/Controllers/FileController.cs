using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using PetFamily.API.Extensions;
using PetFamily.Application.Photos.UploadPhoto;
using PetFamily.Infrastructure.Options;

namespace PetFamily.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly MinioOptions _options;
        private readonly IMinioClient _client;

        public FileController(IOptionsMonitor<MinioOptions> options, IMinioClient client)
        {
            _options = options.CurrentValue;
            _client = client;
        }

        // получение файла
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var buckets = await _client.ListBucketsAsync();

            var bucketsString = string.Join(" ", buckets.Buckets.Select(b => b.Name));
            return Ok(bucketsString);
        }

        // создание файла
        [HttpPost]
        public async Task<ActionResult> Create(
            IFormFile file,
            [FromServices] UploadPhotoService uploadPhotoService,
            CancellationToken cancellation)
        {
            await using var stream = file.OpenReadStream();

            var fileName = Guid.NewGuid().ToString();

            var fileData = new UploadPhotoRequest(stream, "photos", fileName);

            var result = await uploadPhotoService.Upload(fileData, cancellation);
            if (result.IsFailure)
            {
                return result.Error.ToResponse();
            }

            return Ok(result);
        }
    }
}
