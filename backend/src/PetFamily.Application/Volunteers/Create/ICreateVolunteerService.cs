﻿using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Application.Volunteers.CreateVolunteer;

public interface ICreateVolunteerService
{
    Task<CSharpFunctionalExtensions.Result<Guid, Error>> Create(CreateVolunteerRequest createVolunteerRequest,
        CancellationToken ct);
}