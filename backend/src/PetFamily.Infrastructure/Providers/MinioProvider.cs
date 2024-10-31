using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using PetFamily.Application.Database;
using PetFamily.Application.Providers;
using PetFamily.Application.Providers.FileProvider;
using PetFamily.Domain.Pet.PetPhoto;
using PetFamily.Domain.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
        public async Task<Result<IReadOnlyList<FilePath>, Error>> UploadFile(IEnumerable<FileData> filesData, CancellationToken cancellation)
        {
            var semaphoreSlim = new SemaphoreSlim(MAX_DEGREE_PARALLEL);
            var files = filesData.ToList();

            try
            {
                await IfBucketsNotExistCreateBucket(files.Select(f => f.FileInfo.BucketName), cancellation);

                var tasks = files.Select(async file =>
               await PutObject(file, semaphoreSlim, cancellation));

                var pathResult = await Task.WhenAll(tasks);

                if (pathResult.Any(p => p.IsFailure))
                {
                    return pathResult.First().Error;
                }

                var results = pathResult.Select(p => p.Value).ToList();

                return results;
            }
            catch (Exception ex)
            {
                // залогировать для конкретного файла
                _logger.LogError(ex, "Failed to load file to Minio");
                return Error.Failure("upload.file", "Failed to load file to Minio");
            }
        }

        public async Task<Result<FilePath, Error>> PutObject(
            FileData fileData,
            SemaphoreSlim semaphoreSlim,
            CancellationToken cancellation)
        {
            await semaphoreSlim.WaitAsync(cancellation);

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(fileData.FileInfo.BucketName)
                .WithStreamData(fileData.Stream)
                .WithObjectSize(fileData.Stream.Length)
                .WithObject(fileData.FileInfo.filePath.Path);
            try
            {
                var task = _client.PutObjectAsync(putObjectArgs, cancellation);

                semaphoreSlim.Release();

                return fileData.FileInfo.filePath;
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

        public async Task<UnitResult<Error>> RemoveFile(Application.Providers.FileProvider.FileInfo fileInfo, CancellationToken cancellation)
        {
            try
            {
                var statArgs = new StatObjectArgs()
                    .WithBucket(PHOTO)
                    .WithObject(fileInfo.filePath.Path);

                var objectStat = await _client.StatObjectAsync(statArgs);

                if (objectStat == null)
                {
                    return Result.Success<Error>();
                }

                var rmArgs = new RemoveObjectArgs()
                    .WithBucket(PHOTO)
                    .WithObject(fileInfo.filePath.Path);

                await _client.RemoveObjectAsync(rmArgs);

                _logger.LogInformation("{fileName} file successfully deleted ", fileInfo.filePath.Path);
                return Result.Success<Error>();

            }
            catch (MinioException e)
            {
                _logger.LogInformation("Failed to delete file {fileName} from S3 storage", fileInfo.filePath.Path);
                return Error.Failure("delete.file", "Failed to delete file from S3 storage");
            }

        }

        private async Task IfBucketsNotExistCreateBucket(
        IEnumerable<string> buckets,
        CancellationToken cancellationToken)
        {
            HashSet<string> bucketNames = [.. buckets];

            foreach (var bucketName in bucketNames)
            {
                var bucketExistArgs = new BucketExistsArgs()
                    .WithBucket(bucketName);
                var bucketExist = await _client
                    .BucketExistsAsync(bucketExistArgs, cancellationToken);
                if (bucketExist == false)
                {
                    var makeBucketArgs = new MakeBucketArgs()
                        .WithBucket(bucketName);
                    await _client.MakeBucketAsync(makeBucketArgs, cancellationToken);
                }
            }
        }
    }
}
