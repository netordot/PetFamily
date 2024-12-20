﻿using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Application.PetManagement.Commands.Volunteers;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel.Constraints;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PetFamily.Volunteers.Application.Commands.SoftDeletePet;

public class SoftDeletePetHandler : ICommandHandler<Guid, SoftDeletePetCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SoftDeletePetHandler(IVolunteerRepository volunteerRepository,
        [FromKeyedServices(ModuleNames.Volunteers)] IUnitOfWork unitOfWork)
    {
        _volunteerRepository = volunteerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid, ErrorList>> Handle(SoftDeletePetCommand command, CancellationToken cancellation)
    {
        var volunteer = await _volunteerRepository.GetById(command.VolunteerId, cancellation);
        if (volunteer.IsFailure)
        {
            return volunteer.Error.ToErrorList();
        }

        var pet = volunteer.Value.GetPetById(command.PetId);
        if (pet.IsFailure)
        {
            return pet.Error.ToErrorList();
        }

        pet.Value.Delete();
        await _unitOfWork.SaveChanges(cancellation);

        return pet.Value.Id.Value;
    }
}
