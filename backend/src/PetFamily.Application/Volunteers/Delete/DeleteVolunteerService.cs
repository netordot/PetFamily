using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.Delete
{
    public class DeleteVolunteerService : IDeleteVolunteerService
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<DeleteVolunteerService> _logger;

        public DeleteVolunteerService(IVolunteerRepository repository, ILogger<DeleteVolunteerService> logger)
        {
            _volunteerRepository = repository;
            _logger = logger;

        }

        public async Task<Result<Guid, Error>> Delete(DeleteVolunteerRequest request, CancellationToken cancellationToken)
        {
            var volunteerResult = await _volunteerRepository.GetById(request.Id, cancellationToken);
            if (volunteerResult.IsFailure)
            {
                return volunteerResult.Error;
            }

            var result = await _volunteerRepository.Delete(volunteerResult.Value, cancellationToken);

            _logger.LogInformation("Updated deleted Volunteer with Id {result}", result);

            return result;
        }

    }
}
