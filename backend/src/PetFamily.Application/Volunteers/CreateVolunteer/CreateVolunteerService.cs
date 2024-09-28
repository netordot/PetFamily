using PetFamily.Domain;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.PhoneNumber;
using PetFamily.Domain.Volunteer;

namespace PetFamily.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerService : ICreateVolunteerService
{
    private readonly IVolunteerRepository _volunteerRepository;

    public CreateVolunteerService(IVolunteerRepository volunteerRepository )
    {
        _volunteerRepository = volunteerRepository;
    }
    
    public async Task<CSharpFunctionalExtensions.Result<Guid, string>> Create(CreateVolunteerRequest createVolunteerRequest, CancellationToken ct)
    {
        var volunteerId = VolunteerId.NewVolunteerId;
        
        var resultName = new FullName(createVolunteerRequest.FirstName,
            createVolunteerRequest.MiddleName, 
            createVolunteerRequest.LastName);

        var addressResult = new Address(createVolunteerRequest.City, createVolunteerRequest.Street,
            createVolunteerRequest.BuildingNumber,
            createVolunteerRequest.CorpsNumber);

        var phoneResult = PhoneNumber.Create(createVolunteerRequest.PhoneNumber);
        if (phoneResult.IsFailure)
            return "Некоректно введен номер телефона";


        var requisitesResult =
            Requisites.Create(createVolunteerRequest.Requisites.Select(r => Requisite.Create(r.Title, r.Description)).ToList());
        
        var socialsResult = VolunteerDetails.
            Create(createVolunteerRequest.SocialNetworks.Select(s => Social.Create(s.Name, s.Link)).ToList());
        
        var emailResult = Email.Create(createVolunteerRequest.Email);
        if (emailResult.IsFailure)
            return "Email введен некорректно";
        
        var volunteer = Domain.Volunteer.Volunteer.Create(resultName, emailResult.Value,
            createVolunteerRequest.Description, createVolunteerRequest.Experience, phoneResult.Value, null, 
            addressResult, requisitesResult,
            socialsResult, volunteerId);
        
        if (volunteer.IsFailure)
            return "Проблема в создании волонтера";
                
        Volunteer result = volunteer.Value;
        
        return await _volunteerRepository.Add(result, CancellationToken.None);
        
    }
}