using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetFamily.Application.Providers;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Domain.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.Providers
{
    public class MinioProvider : IFileProvider
    {
        private readonly IMinioClient _client;
        private readonly ILogger<MinioProvider> _logger;

        public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
        {
            _client = minioClient;
            _logger = logger;
        }
        public async Task<Result<string, Error>> UploadFile(FileContent content, CancellationToken cancellation)
        {

            try
            {
                var path = Guid.NewGuid();

                var bucketExistsArgs = new BucketExistsArgs()
                    .WithBucket("photos");

                var bucketExists = await _client.BucketExistsAsync(bucketExistsArgs);

                if (bucketExists == false)
                {
                    var makeBucketArgs = new MakeBucketArgs()
                        .WithBucket("photos");

                    await _client.MakeBucketAsync(makeBucketArgs);
                }

                var putObjectArgs = new PutObjectArgs()
                    .WithBucket("photos")
                    .WithStreamData(content.Stream)
                    .WithObjectSize(content.Stream.Length)
                    .WithObject(path.ToString());

                var result = await _client.PutObjectAsync(putObjectArgs);

                return result.ObjectName;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load file to Minio");
                return Error.Failure("upload.file", "Failed to load file to Minio");
            }

        }

    }
}
