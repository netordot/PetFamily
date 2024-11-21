using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Species.Application;
using PetFamily.Species.Application.Commands.DeleteBreed;
using PetFamily.Volunteers.Presentation;

namespace PetFamily.Application.Species.DeleteBreed
{
    public class DeleteBreedHandler : ICommandHandler<Guid, DeleteBreedCommand>
    {
        private readonly IReadDbContext _readDbCOntext;
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVolunteersContract _volunteersContract;

        public DeleteBreedHandler(IReadDbContext context,
            ISpeciesRepository speciesRepository, 
            IUnitOfWork unitOfWork,
            IVolunteersContract volunteersContract)
        {
            _readDbCOntext = context;
            _speciesRepository = speciesRepository;
            _unitOfWork = unitOfWork;
            _volunteersContract = volunteersContract;
        }

        public async Task<Result<Guid, ErrorList>> Handle(DeleteBreedCommand command, CancellationToken cancellation)
        {
            // проверить есть ли вообще такая порода
            //есть, смотрим, используется ли она
            // если она используется, то возвращаем ошибку конфликт
            // если не используется, тогда удаляем 

            var breedExists = await _readDbCOntext.Breeds.FirstOrDefaultAsync(b => b.Id == command.BreedId);
            if (breedExists == null)
            {
                return command.BreedId;
            }

            // должны быть тут контракты с Volunteers module
            var attachedPets = await _volunteersContract.IsBreedInUse(command.BreedId, cancellation);
            if (attachedPets == false)
            {
                // удаляем
                //var result = await _speciesRepository.DeleteBreedById(command.Speciesid,command.BreedId, cancellation);
                //if(result.IsFailure)
                //{
                //    return result.Error.ToErrorList();
                //}
                var species = await _speciesRepository.GetById(command.Speciesid, cancellation);
                var breed = species.Value.Breeds.Find(b => b.Id.Value == command.BreedId);

                species.Value.DeleteBreed(breed);
                await _speciesRepository.Save(species.Value, cancellation);
                _unitOfWork.SaveChanges(cancellation);

               return command.BreedId;
            }

            var breedIsInUse = Error.Conflict("has.dependencies", "the value has dependent values");

            return breedIsInUse.ToErrorList();

        }
    }
}
