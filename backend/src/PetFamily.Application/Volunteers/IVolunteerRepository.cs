﻿using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Volunteer;

namespace PetFamily.Application.Volunteers;

public interface IVolunteerRepository
{
     Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken = default);
     Task<Result<Volunteer,Error>> GetById(Guid volunteerId, CancellationToken cancellationToken = default);
}