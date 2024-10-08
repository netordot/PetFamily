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

    public CreateVolunteerService(IVolunteerRepository volunteerRepository)
    {
        _volunteerRepository = volunteerRepository;
    }

    public async Task<CSharpFunctionalExtensions.Result<Guid, Error>> Create(
        CreateVolunteerRequest createVolunteerRequest, CancellationToken ct)
    {
        var volunteerId = VolunteerId.NewVolunteerId;

        var resultName = new FullName(createVolunteerRequest.FirstName,
            createVolunteerRequest.MiddleName,
            createVolunteerRequest.LastName);

        var addressResult = new Address(createVolunteerRequest.City, createVolunteerRequest.Street,
            createVolunteerRequest.BuildingNumber,
            createVolunteerRequest.CorpsNumber);

        var phoneResult = PhoneNumber.Create(createVolunteerRequest.PhoneNumber);

        var requisites =
            createVolunteerRequest.Requisites.Select(r => Requisite.Create(r.Title, r.Description)).ToList();

        var requisitesResult = new Requisites(requisites.Select(x => x.Value).ToList());

        var socialList =
            createVolunteerRequest.SocialNetworks.Select(s => Social.Create(s.Name, s.Link)).ToList();

        var socialsResult = new VolunteerDetails(socialList.Select(s => s.Value).ToList());

        var emailResult = Email.Create(createVolunteerRequest.Email);

        var volunteer = Domain.Volunteer.Volunteer.Create(resultName, emailResult.Value,
            createVolunteerRequest.Description, createVolunteerRequest.Experience, phoneResult.Value, null,
            addressResult, requisitesResult,
            socialsResult, volunteerId);
        
        Volunteer result = volunteer.Value;

        return await _volunteerRepository.Add(result, CancellationToken.None);
    }
}