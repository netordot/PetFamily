using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
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

        public async Task<Result<string, Error>> GetFile(GetFileProvider provider, CancellationToken cancellation)
        {
            try
            {
                PresignedGetObjectArgs args = new PresignedGetObjectArgs()
                                      .WithBucket("photos")
                                      .WithObject(provider.FileName)
                                      .WithExpiry(60 * 60 * 24);
                string url = await _client.PresignedGetObjectAsync(args);
                if(url ==null)
                {
                    return Errors.General.NotFound();
                }
                return url;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get file from MinIO");
                return Error.Failure("get.file", "Failed to get file from MinIO");
            }
        }

        public async Task<Result<string,Error>> RemoveFile(string fileName, CancellationToken cancellation)
        {
            try
            {
                RemoveObjectArgs rmArgs = new RemoveObjectArgs()
                                              .WithBucket("photos")
                                              .WithObject(fileName);
                await _client.RemoveObjectAsync(rmArgs);
                //логировать ниже
                return fileName;  //Console.WriteLine("successfully removed mybucket/myobject");

            }
            catch (MinioException e)
            {
                //логировать ниже
                //Console.WriteLine("Error: " + e);
                return Error.Failure("delete.file", "Failed to delete file from S3 storage");
            }
        }

    }
}
