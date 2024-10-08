using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Domain;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Shared.Mails;
using PetFamily.Domain.Shared.PhoneNumber;
using PetFamily.Domain.Volunteer;
using PetFamily.Domain.Volunteer.Details;

namespace PetFamily.Application.Volunteers.Create;

public class CreateVolunteerService : ICreateVolunteerService
{
    private readonly IVolunteerRepository _volunteerRepository;
    // private readonly IValidator<CreateVolunteerRequest> _validator;

    public CreateVolunteerService(IVolunteerRepository volunteerRepository)
    {
        _volunteerRepository = volunteerRepository;
        // _validator = validator;
    }

    public async Task<CSharpFunctionalExtensions.Result<Guid, Error>> Create(
        CreateVolunteerRequest createVolunteerRequest, CancellationToken ct)
    {
        // var validationResult = await _validator.ValidateAsync(createVolunteerRequest);
        // if (!validationResult.IsValid)
        // {
        //     var error = Error.Validation(
        //         validationResult.Errors[0].ErrorCode,
        //         validationResult.Errors[0].ErrorMessage);
        //     return error;
        // }

        var volunteerId = VolunteerId.NewVolunteerId;

        var resultName = new FullName(createVolunteerRequest.FirstName,
            createVolunteerRequest.MiddleName,
            createVolunteerRequest.LastName);

        var addressResult = new Address(createVolunteerRequest.City, createVolunteerRequest.Street,
            createVolunteerRequest.BuildingNumber,
            createVolunteerRequest.CorpsNumber);

        var phoneResult = PhoneNumber.Create(createVolunteerRequest.PhoneNumber);
        // if (phoneResult.IsFailure)
        //     return Error.Validation("value.is.invalid", "некорректный номер телефона");


        var requisites =
            createVolunteerRequest.Requisites.Select(r => Requisite.Create(r.Title, r.Description)).ToList();
        // if (requisites.Any(x => x.IsFailure))
        // {
        //     return Error.Validation("value.is.invalid", "некорректно введены реквизиты");
        // }

        var requisitesResult = new Requisites(requisites.Select(x => x.Value).ToList());

        var socialList =
            createVolunteerRequest.SocialNetworks.Select(s => Social.Create(s.Name, s.Link)).ToList();
        // if (socialList.Any(s => s.IsFailure))
        // {
        //     return Error.Validation("value.is.invalid", "некорректно введены соцсети");
        // }

        var socialsResult = new VolunteerDetails(socialList.Select(s => s.Value).ToList());

        var emailResult = Email.Create(createVolunteerRequest.Email);
        // if (emailResult.IsFailure)
        //     return Error.Validation("value.is.invalid", "некорректный Email");

        var volunteer = Domain.Volunteer.Volunteer.Create(resultName, emailResult.Value,
            createVolunteerRequest.Description, createVolunteerRequest.Experience, phoneResult.Value, null,
            addressResult, requisitesResult,
            socialsResult, volunteerId);

        // if (volunteer.IsFailure)
        //     return Error.Failure("unable.create", "проблема при создании волонтера");

        Volunteer result = volunteer.Value;

        return await _volunteerRepository.Add(result, CancellationToken.None);
    }
}