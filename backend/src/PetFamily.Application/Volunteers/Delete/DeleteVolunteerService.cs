using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Application.Volunteers.Create;
using PetFamily.Domain.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Volunteers.Delete
{
    public class DeleteVolunteerService 
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<DeleteVolunteerService> _logger;
        private readonly IValidator<DeleteVolunteerCommand> _validator;


        public DeleteVolunteerService(
            IVolunteerRepository repository, 
            ILogger<DeleteVolunteerService> logger, 
            IValidator<DeleteVolunteerCommand> validator)
        {
            _volunteerRepository = repository;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<Guid, ErrorList>> Delete(DeleteVolunteerCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if(validationResult.IsValid ==false)
            {
                return validationResult.ToErrorList();
            }

            var volunteerResult = await _volunteerRepository.GetById(command.Id, cancellationToken);
            if (volunteerResult.IsFailure)
            {
                return new ErrorList([volunteerResult.Error]);
            }

            var result =  _volunteerRepository.Delete(volunteerResult.Value, cancellationToken);

            _logger.LogInformation("Updated deleted Volunteer with Id {result}", result);

            return result;
        }

    }
}
