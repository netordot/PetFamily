using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Photos.UploadPhoto
{
    public record UploadPhotoRequest(Stream Stream, string BucketName, string ObjectName);
   
}
