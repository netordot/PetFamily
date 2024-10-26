using FluentValidation;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Domain;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Shared.Mails;
using PetFamily.Domain.Shared.PhoneNumber;
using PetFamily.Domain.Shared.Requisites;
using PetFamily.Domain.Volunteer;
using PetFamily.Domain.Volunteer.Details;

namespace PetFamily.Application.Volunteers.Create;

public class CreateVolunteerService 
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreateVolunteerCommand> _validator;

    public CreateVolunteerService(IVolunteerRepository volunteerRepository, IUnitOfWork unitOfWork, IValidator<CreateVolunteerCommand> validator)
    {
        _volunteerRepository = volunteerRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<CSharpFunctionalExtensions.Result<Guid, ErrorList>> Create(
        CreateVolunteerCommand command, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(command, ct);  
        if(validationResult.IsValid == false)
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

        var requisites =
            command.Requisites.Select(r => Requisite.Create(r.Title, r.Description)).ToList();

        var requisitesResult = new Requisites(requisites.Select(x => x.Value).ToList());

        var socialList =
            command.SocialNetworks.Select(s => Social.Create(s.Name, s.Link)).ToList();

        var socialsResult = new VolunteerDetails(socialList.Select(s => s.Value).ToList());

        var emailResult = Email.Create(command.Email).Value;

        var volunteer = Domain.Volunteer.Volunteer.Create(resultName, emailResult,
            command.Description, command.Experience, phoneResult, null,
            addressResult, requisitesResult,
            socialsResult, volunteerId);

        Volunteer volunteerResult = volunteer.Value;

        var result = await _volunteerRepository.Add(volunteerResult, CancellationToken.None);

        await _unitOfWork.SaveChanges(ct);

        return result;
    }
}