using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel.Constraints;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.VolunteerRequest.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Application.Commands.UpdateVolunteerRequest
{
    public class UpdateVolunteerRequestHandler : ICommandHandler<UpdateVolunteerRequestCommand>
    {
        private readonly IVolunteerRequestRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateVolunteerRequestHandler(IVolunteerRequestRepository repository, [FromKeyedServices(ModuleNames.Accounts)]IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<UnitResult<ErrorList>> Handle(UpdateVolunteerRequestCommand command, CancellationToken cancellation)
        {
            // валидация 

            var volunteerRequest = await _repository.GetById(command.VolunteerRequestId, cancellation);
            if(volunteerRequest.IsFailure)
            {
                return volunteerRequest.Error.ToErrorList();
            }

            var email = Email.Create(command.VolunteerInfoDto.Email).Value;
            var phoneNumber = PhoneNumber.Create(command.VolunteerInfoDto.PhoneNumber).Value;
            var fullName = new FullName(
                command.VolunteerInfoDto.FirstName,
                command.VolunteerInfoDto.SecondName,
                command.VolunteerInfoDto.LastName);

            var requistes = command.VolunteerInfoDto.Requisites
                .Select(r => new Requisite(r.Title, r.Description)).ToList();

            var volunteerInfo = new VolunteerRequestInfo(
                fullName,
                command.VolunteerInfoDto.Experience,
                email,
                phoneNumber,
                command.VolunteerInfoDto.Description,
                requistes
                );


            var result = volunteerRequest.Value.ResendAfterRevision(command.UserId, volunteerInfo);
            if(result.IsFailure)
            {
                return result.Error.ToErrorList();
            }

            await _unitOfWork.SaveChanges(cancellation);

            return Result.Success<ErrorList>();
        }
    }
}
