using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.PetManagement.Commands.Volunteers;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel.Constraints;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Application;
using PetFamily.Volunteers.Application.Commands.SetPetMainPhoto;
using PetFamily.Volunteers.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.PetManagement.Commands.Volunteers.SetPetMainPhoto
{
    public class SetPetMainPhotoHandler : ICommandHandler<Guid, SetPetMainPhotoCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SetPetMainPhotoHandler(IVolunteerRepository volunteerRepository, [FromKeyedServices(ModuleNames.Volunteers)] IUnitOfWork unitOfWork)
        {
            _volunteerRepository = volunteerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid, ErrorList>> Handle(SetPetMainPhotoCommand command, CancellationToken cancellation)
        {
            var volunteer = await _volunteerRepository.GetById(command.VolunteerId);
            if (volunteer.IsFailure)
            {
                return volunteer.Error.ToErrorList();
            }

            var pet = volunteer.Value.GetPetById(command.PetId);
            if (pet.IsFailure)
            {
                return pet.Error.ToErrorList();
            }
            // есть смысл проверить если ли такая фотка у пета вообще
            if (pet.Value.Photos.Any(p => p.Path.Path == command.Path) == false)
            {
                return Errors.General.NotFound().ToErrorList();
            }

            var mainPhoto = PetPhoto.Create(FilePath.Create(command.Path).Value, true);

            pet.Value.SetMainPhoto(mainPhoto.Value);
            await _unitOfWork.SaveChanges(cancellation);

            return pet.Value.Id.Value;

        }
    }
}
