using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Species.Application;
using PetFamily.Volunteers.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Species.Application.Commands.DeleteSpecies
{
    public class DeleteSpeciesHandler : ICommandHandler<Guid, DeleteSpeciesCommand>
    {
        private readonly IReadDbContext _readDbContext;
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVolunteersContract _volunteerContract;

        public DeleteSpeciesHandler(IReadDbContext context, 
            ISpeciesRepository species, 
            IUnitOfWork unitOfWork, 
            IVolunteersContract volunteersContract)
        {
            _readDbContext = context;
            _speciesRepository = species;
            _unitOfWork = unitOfWork;
            _volunteerContract = volunteersContract;
        }
        // контракт с VolunteersModule
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

            var attachedPets = await _volunteerContract.IsSpeciesInUse(command.id, cancellation);
            if (attachedPets == false)
            {
                await _speciesRepository.Delete(command.id, cancellation);
                await _unitOfWork.SaveChanges(cancellation);
                return command.id;
            }

            return Error.Conflict("has.dependent.values", "value can not be deleted as it has dependent values").ToErrorList();
        }
    }
}
