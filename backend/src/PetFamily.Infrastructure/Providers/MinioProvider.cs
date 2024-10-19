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
        private readonly string PHOTO = "photos";
        public const int MAX_DEGREE_PARALLEL = 10;

        public MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger)
        {
            _client = minioClient;
            _logger = logger;
        }
        public async Task<UnitResult<Error>> UploadFile(FileData fileData, CancellationToken cancellation)
        {
            var semaphoreSlim = new SemaphoreSlim(MAX_DEGREE_PARALLEL);

            try
            {
                await BucketExists(fileData.BucketName, cancellation);
                //var bucketExistsArgs = new BucketExistsArgs()
                //    .WithBucket(fileData.BucketName);

                //var bucketExists = await _client.BucketExistsAsync(bucketExistsArgs);

                //if (bucketExists == false)
                //{
                //    var makeBucketArgs = new MakeBucketArgs()
                //        .WithBucket(fileData.BucketName);

                //    await _client.MakeBucketAsync(makeBucketArgs);
                //}

                List<Task> tasks = [];

                foreach (var file in fileData.Files)
                {
                    await semaphoreSlim.WaitAsync(cancellation);

                    var putObjectArgs = new PutObjectArgs()
                        .WithBucket(fileData.BucketName)
                        .WithStreamData(file.Stream)
                        .WithObjectSize(file.Stream.Length)
                        .WithObject(file.ObjectName);

                    var task = _client.PutObjectAsync(putObjectArgs, cancellation);

                    semaphoreSlim.Release();
                    tasks.Add(task);
                }

                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load file to Minio");
                return Error.Failure("upload.file", "Failed to load file to Minio");
            }
            finally
            {
                semaphoreSlim.Release();
            }

            return Result.Success<Error>();

        }

        public async Task<Result<string, Error>> GetFile(GetFileProvider provider, CancellationToken cancellation)
        {
            try
            {
                var statObjectArgs = new StatObjectArgs()
                    .WithBucket(PHOTO)
                    .WithObject(provider.FileName);

                var objStat = await _client.StatObjectAsync(statObjectArgs);


                PresignedGetObjectArgs args = new PresignedGetObjectArgs()
                                      .WithBucket(PHOTO)
                                      .WithObject(provider.FileName)
                                      .WithExpiry(60 * 60 * 24);

                string url = await _client.PresignedGetObjectAsync(args);

                return url;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get file from MinIO");
                return Error.Failure("get.file", "Failed to get file from MinIO");
            }
        }

        public async Task<Result<string, Error>> RemoveFile(string fileName, CancellationToken cancellation)
        {
            try
            {
                RemoveObjectArgs rmArgs = new RemoveObjectArgs()
                                              .WithBucket(PHOTO)
                                              .WithObject(fileName);
                await _client.RemoveObjectAsync(rmArgs);

                _logger.LogInformation("{fileName} file successfully deleted ", fileName);
                return fileName;

            }
            catch (MinioException e)
            {
                _logger.LogInformation("Failed to delete file {fileName} from S3 storage", fileName);
                return Error.Failure("delete.file", "Failed to delete file from S3 storage");
            }

        }
        private async Task<UnitResult<Error>> BucketExists(string bucketName, CancellationToken cancellationToken)
        {
            try
            {
                var bucketExistsArgs = new BucketExistsArgs()
                    .WithBucket(bucketName);

                if (await _client.BucketExistsAsync(bucketExistsArgs, cancellationToken) == false)
                {
                    var makeBucketArgs = new MakeBucketArgs()
                          .WithBucket(bucketName);

                    await _client.MakeBucketAsync(makeBucketArgs);
                }

            }

            catch (MinioException e)
            {
                _logger.LogError(e, bucketName);
                return Error.Failure("create.or.find", "unable to create or find bucket");
            }

            return UnitResult.Success<Error>();
        }
    }
}
