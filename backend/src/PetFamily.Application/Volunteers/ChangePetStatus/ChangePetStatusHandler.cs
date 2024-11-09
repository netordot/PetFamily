using CSharpFunctionalExtensions;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Domain.Pet;
using PetFamily.Domain.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.ChangePetStatus
{
    public class ChangePetStatusHandler : ICommandHandler<Guid, ChangePetStatusCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangePetStatusHandler(IVolunteerRepository volunteerRepository, IUnitOfWork unitOfWork)
        {
            _volunteerRepository = volunteerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid, ErrorList>> Handle(ChangePetStatusCommand command, CancellationToken cancellation)
        {
            var volunteer = await _volunteerRepository.GetById(command.VolunteerId);
            if (volunteer.IsFailure)
            {
                return volunteer.Error.ToErrorList();
            }

            var pet = volunteer.Value.GetPetById(command.PetId);
            if (pet.IsFailure)
            {
                return command.PetId;
            }

            if(command.Status == Dtos.PetStatusDto.Adopted)
            {
                return Errors.General.ValueIsInvalid("status").ToErrorList();
            }

            var status = (PetStatus)(int)command.Status;
            pet.Value.SetStatus(status);

            _volunteerRepository.Save(volunteer.Value);
            await _unitOfWork.SaveChanges(cancellation);

            return pet.Value.Id.Value;
        }
    }
}
