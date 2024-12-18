using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Providers;
using PetFamily.SharedKernel.Constraints;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Application.Commands.EditMessage
{
    public class EditMessageHandler : ICommandHandler<EditMessageCommand>
    {
        private readonly IDiscussionsRepository _discussionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EditMessageHandler(IDiscussionsRepository discussionsRepository,
            [FromKeyedServices(ModuleNames.Discussion)] IUnitOfWork unitOfWork)
        {
            _discussionRepository = discussionsRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<UnitResult<ErrorList>> Handle(EditMessageCommand command, CancellationToken cancellation)
        {
            var discussion = await _discussionRepository.GetById(command.DiscussionId, cancellation);
            if (discussion.IsFailure)
            {
                return discussion.Error.ToErrorList();
            }

            var editResult = discussion.Value.EditComment(command.MessageId, command.UserId, command.Text);
            if (editResult.IsFailure)
            {
                return editResult.Error.ToErrorList();
            }

            await _unitOfWork.SaveChanges(cancellation);

            return Result.Success<ErrorList>();
        }
    }
}
