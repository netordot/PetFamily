using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Errors;

namespace PetFamily.Application.Volunteers.UpdateRequisites;

public class UpdateRequisitesService : IUpdateRequisitesService
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateRequisitesService> _logger;

    public UpdateRequisitesService(IVolunteerRepository volunteerRepository, ILogger<UpdateRequisitesService> logger)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> UdpateRequisites(UpdateRequisitesRequest request, CancellationToken ct)
    {
        var volunteerResult = await _volunteerRepository.GetById(request.Id, ct);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var requisiteList = request.ListDto.requisites.Select(r => Requisite.Create(r.Title, r.Description)).ToList();
        var requisitesResult = new Requisites(requisiteList.Select(r => r.Value).ToList());

        volunteerResult.Value.UdpateRequisites(requisitesResult);

        return await _volunteerRepository.Save(volunteerResult.Value, ct);
    }
}