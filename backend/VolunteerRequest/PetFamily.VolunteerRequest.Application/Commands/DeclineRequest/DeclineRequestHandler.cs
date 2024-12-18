using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Providers;
using PetFamily.Discussion.Contracts;
using PetFamily.SharedKernel.Constraints;
using PetFamily.SharedKernel.Id;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.VolunteerRequest.Domain.AggregateRoot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Application.Commands.DeclineRequest
{
    public class DeclineRequestHandler : ICommandHandler<DeclineRequestCommand>
    {
        private readonly IVolunteerRequestRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDiscussionContract _discussionContract;
        private readonly IUserRestrictionsRepository _userRestrictionsRepository;

        public DeclineRequestHandler(IVolunteerRequestRepository repository,
            [FromKeyedServices(ModuleNames.VolunteerRequest)] IUnitOfWork unitOfWork,
            IDiscussionContract discussionContract,
            IUserRestrictionsRepository userRestrictionsRepository)

        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _discussionContract = discussionContract;
            _userRestrictionsRepository = userRestrictionsRepository;
        }

        public async Task<UnitResult<ErrorList>> Handle(DeclineRequestCommand command, CancellationToken cancellation)
        {
            // валидация

            var volunteerRequest = await _repository.GetById(command.RequestId, cancellation);
            if (volunteerRequest.IsFailure)
            {
                return volunteerRequest.Error.ToErrorList();
            }

            var deleteResult = volunteerRequest.Value.Decline();
            if (deleteResult.IsFailure)
            {
                return deleteResult.Error.ToErrorList();
            }

            await _unitOfWork.SaveChanges(cancellation);

            var discussionResult = await _discussionContract.CloseDiscussionById(volunteerRequest.Value.DiscussionId, command.AdminId);
            if (discussionResult.IsFailure)
            {
                return discussionResult.Error;
            }

            var discussionId = volunteerRequest.Value.DiscussionId;
            var userRestrictionId = UserRestrictionId.NewUserRestrictionId;

            var userRestriction = UserRestriction.Create(discussionId, UserRestriction.DEFAULT_BAN_DAYS, command.RejectionDescription, userRestrictionId).Value;

            var result = await _userRestrictionsRepository.Add(userRestriction, cancellation);
            await _unitOfWork.SaveChanges(cancellation);

            return UnitResult.Success<ErrorList>();
        }
    }
}
