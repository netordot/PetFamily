using PetFamily.Application.PetManagement.Commands.Volunteers.AddPet.AddPhoto;

namespace PetFamily.API.Processors
{
    public class FormFileProcessor : IAsyncDisposable
    {
        private readonly List<FileDto> _fileDtos = [];
        
        public List<FileDto> Process(IFormFileCollection files)
        {
            foreach(var file in files)
            {

                var stream = file.OpenReadStream();
                var fileDto = new FileDto(stream, file.Name, file.ContentType);
                _fileDtos.Add(fileDto); 
            }

            return _fileDtos;
        }

        public async ValueTask DisposeAsync()
        {
            foreach (var file in _fileDtos)
            {
                await file.stream.DisposeAsync();
            }
        }
          
        
    }
}
