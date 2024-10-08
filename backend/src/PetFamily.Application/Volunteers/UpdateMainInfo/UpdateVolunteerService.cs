﻿using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Shared.Mails;
using PetFamily.Domain.Shared.PhoneNumber;
using PetFamily.Domain.Volunteer;

namespace PetFamily.Application.Volunteers.UpdateVolunteer;

public class UpdateVolunteerService : IUpdateVolunteerService
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateVolunteerService> _logger;

    public UpdateVolunteerService(IVolunteerRepository volunteerRepository, ILogger<UpdateVolunteerService> logger)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Update(UpdateVolunteerRequest request, CancellationToken cancellationToken)
    {
        var volunteerResult = await _volunteerRepository.GetById(request.id, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var phoneNumber = PhoneNumber.Create(request.dto.PhoneNumber);
        var name = new FullName(request.dto.FirstName, request.dto.MiddleName, request.dto.LastName);
        var email = Email.Create(request.dto.Email);
        var address = new Address(request.dto.City, request.dto.Street, request.dto.BuildingNumber,
            request.dto.CorpsNumber);

        volunteerResult.Value.UpdateMainInfo(name, email.Value, request.dto.Description, request.dto.Experience,
            phoneNumber.Value, address);

        await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);

        _logger.LogInformation("Updated Volunteer with Id  {request.id}", request.id);

        return request.id;
    }
}
