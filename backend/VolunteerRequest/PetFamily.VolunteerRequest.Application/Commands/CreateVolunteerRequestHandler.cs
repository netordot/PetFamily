using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel.Constraints;
using PetFamily.SharedKernel.Id;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.VolunteerRequest.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Application.Commands
{
    public class CreateVolunteerRequestHandler : ICommandHandler<CreateVolunteerRequestCommand>
    {
        private readonly IVolunteerRequestRepository _requestRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateVolunteerRequestHandler(IVolunteerRequestRepository repository, 
            [FromKeyedServices(ModuleNames.VolunteerRequest)]IUnitOfWork unitOfWork)
        {
            _requestRepository = repository;  
            _unitOfWork = unitOfWork;
        }
        public async Task<UnitResult<ErrorList>> Handle(CreateVolunteerRequestCommand command, CancellationToken cancellation)
        {
            // валидация 

            var request = await _requestRepository.GetByUserId(command.ParticipantId, cancellation);
            if (request.IsSuccess)
            {
                return Errors.General.AlreadyExists("request").ToErrorList();
            }

            var email = Email.Create(command.VolunteerInfo.Email).Value;
            var phoneNumber = PhoneNumber.Create(command.VolunteerInfo.PhoneNumber).Value;
            var fullName = new FullName(
                command.VolunteerInfo.FirstName,
                command.VolunteerInfo.SecondName,
                command.VolunteerInfo.LastName);

            var requistes = command.VolunteerInfo.Requisites
                .Select(r => new Requisite(r.Title, r.Description)).ToList();



            var volunteerInfo = new VolunteerRequestInfo(
                fullName,
                command.VolunteerInfo.Experience,
                email,
                phoneNumber,
                command.VolunteerInfo.Description,
                requistes
                );

            var volunteerRequest  = VolunteerRequest.Domain.AggregateRoot.VolunteerRequest.Create
                (VolunteerRequestId.NewVolunteerRequestId,
                command.ParticipantId,
                volunteerInfo,
                DateTime.UtcNow).Value;

            await _requestRepository.Add(volunteerRequest, cancellation);

            await _unitOfWork.SaveChanges(cancellation);

            return Result.Success<ErrorList>();

        }
    }
}
