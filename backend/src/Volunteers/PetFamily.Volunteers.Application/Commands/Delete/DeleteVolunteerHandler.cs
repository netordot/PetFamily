using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Extensions;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Volunteers.Application.Commands.Delete
{
    public class DeleteVolunteerHandler : ICommandHandler<Guid, DeleteVolunteerCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger<DeleteVolunteerHandler> _logger;
        private readonly IValidator<DeleteVolunteerCommand> _validator;


        public DeleteVolunteerHandler(
            IVolunteerRepository repository,
            ILogger<DeleteVolunteerHandler> logger,
            IValidator<DeleteVolunteerCommand> validator)
        {
            _volunteerRepository = repository;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<Guid, ErrorList>> Handle(DeleteVolunteerCommand command, CancellationToken cancellationToken)
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

            var result = _volunteerRepository.Delete(volunteerResult.Value, cancellationToken);

            _logger.LogInformation("Updated deleted Volunteer with Id {result}", result);

            return result;
        }

    }
}
