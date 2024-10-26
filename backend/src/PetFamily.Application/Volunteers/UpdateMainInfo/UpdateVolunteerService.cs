using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Shared.Mails;
using PetFamily.Domain.Shared.PhoneNumber;
using PetFamily.Domain.Volunteer;

namespace PetFamily.Application.Volunteers.UpdateMainInfo;

public class UpdateVolunteerService 
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateVolunteerService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateVolunteerCommand> _validator;

    public UpdateVolunteerService(
        IVolunteerRepository volunteerRepository,
        ILogger<UpdateVolunteerService> logger,
        IUnitOfWork unitOfWork,
        IValidator<UpdateVolunteerCommand> validator)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Update(UpdateVolunteerCommand request, CancellationToken cancellationToken)
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
