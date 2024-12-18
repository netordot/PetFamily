using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Accounts.Contracts;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Providers;
using PetFamily.Discussion.Contracts;
using PetFamily.SharedKernel.Constraints;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Application.Commands.ApproveVolunteerRequest
{
    public class ApproveVolunteerRequestHandler : ICommandHandler<ApproveVolunteerRequestCommand>
    {
        private readonly IVolunteerRequestRepository _requestRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRestrictionsRepository _restrictionRepository;
        private readonly IDiscussionContract _discussionContract;
        private readonly IAccountsContract _accountsContract;

        public ApproveVolunteerRequestHandler(
            IVolunteerRequestRepository repository,
            IUserRestrictionsRepository userRestrictionsRepository,
            IDiscussionContract discussionContract,
            IAccountsContract accountsContract,
            [FromKeyedServices(ModuleNames.VolunteerRequest)]IUnitOfWork unitOfWork)
        {
            _requestRepository = repository;
            _unitOfWork = unitOfWork;
            _restrictionRepository = userRestrictionsRepository;
            _discussionContract = discussionContract;
            _accountsContract = accountsContract;
        }

        public async Task<UnitResult<ErrorList>> Handle(ApproveVolunteerRequestCommand command, CancellationToken cancellation)
        {
            // валидация 

            var volunteerRequest = await _requestRepository.GetById(command.VolunteerRequestId, cancellation);
            if (volunteerRequest.IsFailure)
            {
                return volunteerRequest.Error.ToErrorList();
            }

            if (volunteerRequest.Value.AdminId != command.AdminId)
            {
                return Error.Failure("admin.unattached", "admin does not belong to this volunteer request").ToErrorList();
            }

            volunteerRequest.Value.Approve();

            await _unitOfWork.SaveChanges(cancellation);

            // закрывается дискуссия
            var discussion = await _discussionContract.CloseDiscussionById(volunteerRequest.Value.DiscussionId, command.AdminId);
            if (discussion.IsFailure)
            {
                return discussion.Error;
            }

            // через контракт создается волонтерский аккаунт 

            var userId = volunteerRequest.Value.UserId;
            var experience = volunteerRequest.Value.VolunteerInfo.Experience;
            var requisites = volunteerRequest.Value.VolunteerInfo.Requisites;

            var createResult = await _accountsContract.CreateVolunteerAccount(userId, experience, requisites, cancellation);
            if (createResult.IsFailure)
            {
                return createResult.Error;
            }

            await _unitOfWork.SaveChanges(cancellation);

            return Result.Success<ErrorList>();

        }
    }
}
