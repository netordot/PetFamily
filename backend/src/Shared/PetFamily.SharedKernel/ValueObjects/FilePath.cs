using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.SharedKernel.ValueObjects
{
    public record FilePath
    {
        public string Path { get; }

        private FilePath()
        {

        }

        private FilePath(string path)
        {
            Path = path;
        }

        public static Result<FilePath, Error> Create(Guid path, string extension)
        {
            // валидация расширения и самого файла

            var filePath = new FilePath(path.ToString() + "." + extension);

            return filePath;
        }

        public static Result<FilePath, Error> Create(string fullPath)
        {
            var filePath = new FilePath(fullPath);
            return filePath;
        }
    }
}
