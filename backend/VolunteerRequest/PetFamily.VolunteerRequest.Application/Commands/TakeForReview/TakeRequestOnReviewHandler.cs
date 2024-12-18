using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
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

namespace PetFamily.VolunteerRequest.Application.Commands.TakeForReview
{
    public class TakeRequestOnReviewHandler : ICommandHandler<TakeRequestOnReviewCommand>
    {
        private readonly IVolunteerRequestRepository _repository;
        private readonly IDiscussionContract _discussionContract;
        private readonly IUnitOfWork _unitOfWork;

        public TakeRequestOnReviewHandler(
            IVolunteerRequestRepository requestRepository,
            IDiscussionContract discussionContract,
            [FromKeyedServices(ModuleNames.VolunteerRequest)] IUnitOfWork unitOfWork)
        {
            _repository = requestRepository;
            _discussionContract = discussionContract;
            _unitOfWork = unitOfWork;
        }
        public async Task<UnitResult<ErrorList>> Handle(TakeRequestOnReviewCommand command, CancellationToken cancellation)
        {
            // через контракты создаем дискуссию
            // меняем статус на на рассмотрении


            var volunteerRequest = await _repository.GetById(command.VolunteerRequestId, cancellation);
            if (volunteerRequest.IsFailure)
            {
                return volunteerRequest.Error.ToErrorList();
            }

            // через контракты создаем дискуссию

            var transaction = await _unitOfWork.BeginTransaction(cancellation);
            try
            {
                var discussionId = await _discussionContract.CreateDiscussion(volunteerRequest.Value.Id.Value, volunteerRequest.Value.UserId, command.AdminId);
                if (discussionId.IsFailure)
                {
                    return discussionId.Error;
                }

                var result = volunteerRequest.Value.TakeOnReview(command.AdminId, discussionId.Value);
                if (result.IsFailure)
                {
                    return result.Error.ToErrorList();
                }

                await _unitOfWork.SaveChanges(cancellation);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                return Error.Failure("discussion.create" , "unable to create discusison").ToErrorList();
            }



            return Result.Success<ErrorList>();
        }
    }
}
