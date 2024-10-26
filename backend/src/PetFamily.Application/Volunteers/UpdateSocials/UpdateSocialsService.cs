using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Volunteer;
using PetFamily.Domain.Volunteer.Details;

namespace PetFamily.Application.Volunteers.UpdateSocials;

public class UpdateSocialsService 
{
    private readonly IVolunteerRepository _volunteerRepository;

    private readonly ILogger<UpdateSocialsService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateSocialsCommand> _validator;

    public UpdateSocialsService(
        IVolunteerRepository volunteerRepository,
        ILogger<UpdateSocialsService> logger,
        IUnitOfWork context,
        IValidator<UpdateSocialsCommand> validator)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = context;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> UpdateSocials(UpdateSocialsCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if(validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }
        var volunteerResult = await _volunteerRepository.GetById(command.Id, cancellationToken);
        if(volunteerResult.IsFailure)
        {
            return new ErrorList([volunteerResult.Error]);
        }

        var socialList = command.Dto.socials.Select(s => Social.Create(s.Name, s.Link)).ToList();

        var socialsResult = new VolunteerDetails(socialList.Select(s => s.Value).ToList());
        volunteerResult.Value.UpdateSocials(socialsResult);

        await _unitOfWork.SaveChanges(cancellationToken);
        _logger.LogInformation("Updated Volunteer socials with Id {id}", command.Id);

        return command.Id;
    }
}