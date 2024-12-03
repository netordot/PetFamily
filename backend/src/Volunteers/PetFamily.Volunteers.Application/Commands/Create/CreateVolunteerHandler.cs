using FluentValidation;
using PetFamily.Application.Extensions;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel.Id;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Application;
using PetFamily.Volunteers.Domain.AggregateRoot;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Commands.Create;

public class CreateVolunteerHandler : ICommandHandler<Guid, CreateVolunteerCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateVolunteerCommand> _validator;

    public CreateVolunteerHandler(IVolunteerRepository volunteerRepository, IUnitOfWork unitOfWork, IValidator<CreateVolunteerCommand> validator)
    {
        _volunteerRepository = volunteerRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<CSharpFunctionalExtensions.Result<Guid, ErrorList>> Handle(
        CreateVolunteerCommand command, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(command, ct);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }

        var volunteerId = VolunteerId.NewVolunteerId;

        var resultName = new FullName(command.FirstName,
            command.MiddleName,
            command.LastName);

        var addressResult = new Address(command.City, command.Street,
            command.BuildingNumber,
            command.CorpsNumber);

        var phoneResult = PhoneNumber.Create(command.PhoneNumber).Value;

        //var requisites =
        //    command.Requisites.Select(r => Requisite.Create(r.Title, r.Description).Value).ToList();

        ////var requisitesResult = new Requisites(requisites.Select(x => x.Value).ToList());

        //var socialList =
        //    command.SocialNetworks.Select(s => Social.Create(s.Name, s.Link).Value).ToList();

        //var socialsResult = new VolunteerDetails(socialList.Select(s => s.Value).ToList());

        var emailResult = Email.Create(command.Email).Value;

        var volunteer = Volunteer.Create(resultName, emailResult,
            command.Description, command.Experience, phoneResult, null,
            addressResult,  volunteerId);

        Volunteer volunteerResult = volunteer.Value;

        var result = await _volunteerRepository.Add(volunteerResult, CancellationToken.None);

        await _unitOfWork.SaveChanges(ct);

        return result;
    }
}