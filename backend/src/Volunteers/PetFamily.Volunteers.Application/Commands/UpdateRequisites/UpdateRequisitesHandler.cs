using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Application.PetManagement.Commands.Volunteers;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Application;
using PetFamily.Volunteers.Application.Commands.UpdateRequisites;

namespace PetFamily.Application.PetManagement.Commands.Volunteers.UpdateRequisites;

public class UpdateRequisitesHandler : ICommandHandler<Guid, UpdateRequisitesCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateRequisitesHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateRequisitesCommand> _validator;

    public UpdateRequisitesHandler(
        IVolunteerRepository volunteerRepository,
        ILogger<UpdateRequisitesHandler> logger,
        IUnitOfWork unitOfWork,
        IValidator<UpdateRequisitesCommand> validator)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(UpdateRequisitesCommand request, CancellationToken ct)
    {
        var validationResult = await _validator.ValidateAsync(request, ct);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }
        var volunteerResult = await _volunteerRepository.GetById(request.Id, ct);
        if (volunteerResult.IsFailure)
            return new ErrorList([volunteerResult.Error]);

        var requisiteList = request.ListDto.requisites.Select(r => Requisite.Create(r.Title, r.Description).Value).ToList();
        //var requisitesResult = new Requisites(requisiteList.Select(r => r.Value).ToList());

        volunteerResult.Value.UdpateRequisites(requisiteList);

        _logger.LogInformation("Updated Volunteer requisites with Id {id}", request.Id);

        await _unitOfWork.SaveChanges(ct);

        return volunteerResult.Value.Id.Value;
    }
}