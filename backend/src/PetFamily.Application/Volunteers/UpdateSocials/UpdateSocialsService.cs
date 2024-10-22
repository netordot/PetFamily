using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Volunteer;
using PetFamily.Domain.Volunteer.Details;

namespace PetFamily.Application.Volunteers.UpdateSocials;

public class UpdateSocialsService : IUpdateSocialsService
{
    private readonly IVolunteerRepository _volunteerRepository;

    private readonly ILogger<UpdateSocialsService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSocialsService(
        IVolunteerRepository volunteerRepository,
        ILogger<UpdateSocialsService> logger,
        IUnitOfWork context)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = context;
    }

    public async Task<Result<Guid, Error>> UpdateSocials(UpdateSocialsRequest request,
        CancellationToken cancellationToken)
    {
        var volunteerResult = await _volunteerRepository.GetById(request.Id, cancellationToken);

        var socialList = request.Dto.socials.Select(s => Social.Create(s.Name, s.Link)).ToList();

        var socialsResult = new VolunteerDetails(socialList.Select(s => s.Value).ToList());
        volunteerResult.Value.UpdateSocials(socialsResult);

        await _unitOfWork.SaveChanges(cancellationToken);
        _logger.LogInformation("Updated Volunteer socials with Id {id}", request.Id);

        return request.Id;
    }
}