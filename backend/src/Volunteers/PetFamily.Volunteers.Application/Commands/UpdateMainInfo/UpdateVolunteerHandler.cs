using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel.Constraints;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Commands.UpdateMainInfo;

public class UpdateVolunteerHandler : ICommandHandler<Guid, UpdateVolunteerCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateVolunteerHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateVolunteerCommand> _validator;

    public UpdateVolunteerHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<UpdateVolunteerHandler> logger,
       [FromKeyedServices(ModuleNames.Volunteers)] IUnitOfWork unitOfWork,
        IValidator<UpdateVolunteerCommand> validator)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(UpdateVolunteerCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }

        var volunteerResult = await _volunteerRepository.GetById(request.id, cancellationToken);
        if (volunteerResult.IsFailure)
            return new ErrorList([volunteerResult.Error]);

        var phoneNumber = PhoneNumber.Create(request.dto.PhoneNumber);
        var name = new FullName(request.dto.FirstName, request.dto.MiddleName, request.dto.LastName);
        var email = Email.Create(request.dto.Email);
        var address = new Address(request.dto.City, request.dto.Street, request.dto.BuildingNumber,
            request.dto.CorpsNumber);

        volunteerResult.Value.UpdateMainInfo(name, email.Value, request.dto.Description, request.dto.Experience,
            phoneNumber.Value, address);

        await _unitOfWork.SaveChanges(cancellationToken);

        _logger.LogInformation("Updated Volunteer with Id  {request.id}", request.id);

        return request.id;
    }
}
