using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Core.Providers
{
    public record FileInfo(FilePath filePath, string BucketName);
}
