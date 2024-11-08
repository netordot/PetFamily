using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Domain.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Species.DeleteSpecies
{
    public class DeleteSpeciesHandler : ICommandHandler<Guid, DeleteSpeciesCommand>
    {
        private readonly IReadDbContext _readDbContext;
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSpeciesHandler(IReadDbContext context, ISpeciesRepository species, IUnitOfWork unitOfWork)
        {
            _readDbContext = context;
            _speciesRepository = species;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid, ErrorList>> Handle(DeleteSpeciesCommand command, CancellationToken cancellation)
        {
            // проверяем есть ли спишес(если нет возвращаем успех, тк спишеса уже нет)
            // если есть проверяем есть ли животные с таким айдишником
            // если есть вернуть ошибку конфликт
            // если нет, удаляем и кидаем статускод 200

            var species = await _readDbContext.Species.FirstOrDefaultAsync(s => s.Id == command.id);
            if (species == null)
            {
                return command.id;
            }

            var attachedPets = await _readDbContext.Pets.FirstOrDefaultAsync(p => p.SpeciesId == command.id);
            if(attachedPets == null)
            {
                await _speciesRepository.Delete(command.id, cancellation);
                await _unitOfWork.SaveChanges(cancellation);  
                return command.id;
            }

            return Error.Conflict("has.dependent.values", "value can not be deleted as it has dependent values").ToErrorList();
        }
    }
}
