using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Application.PetManagement.Commands.Volunteers.UpdateSocials;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Application;

namespace PetFamily.Volunteers.Application.Commands.UpdateSocials;

public class UpdateSocialsHandler : ICommandHandler<Guid, UpdateSocialsCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;

    private readonly ILogger<UpdateSocialsHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateSocialsCommand> _validator;

    public UpdateSocialsHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<UpdateSocialsHandler> logger,
        IUnitOfWork context,
        IValidator<UpdateSocialsCommand> validator)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = context;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(UpdateSocialsCommand command,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }
        var volunteerResult = await _volunteerRepository.GetById(command.Id, cancellationToken);
        if (volunteerResult.IsFailure)
        {
            return new ErrorList([volunteerResult.Error]);
        }

        var socialList = command.socialsList.Select(s => Social.Create(s.Name, s.Link).Value).ToList();

        //var socialsResult = new VolunteerDetails(socialList.Select(s => s.Value).ToList());

        volunteerResult.Value.UpdateSocials(socialList);

        await _unitOfWork.SaveChanges(cancellationToken);
        _logger.LogInformation("Updated Volunteer socials with Id {id}", command.Id);

        return command.Id;
    }
}