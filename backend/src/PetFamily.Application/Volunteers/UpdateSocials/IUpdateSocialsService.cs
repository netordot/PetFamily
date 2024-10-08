﻿using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Application.Volunteers.UpdateSocials;

public interface IUpdateSocialsService
{
    Task<Result<Guid, Error>> UpdateSocials(UpdateSocialsRequest request,
        CancellationToken cancellationToken);
}