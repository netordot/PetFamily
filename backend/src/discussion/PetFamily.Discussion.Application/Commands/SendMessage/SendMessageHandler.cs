using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Providers;
using PetFamily.Discussion.Domain.Entities;
using PetFamily.SharedKernel.Constraints;
using PetFamily.SharedKernel.Id;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Application.Commands.SendMessage
{
    public class SendMessageHandler : ICommandHandler<SendMessageCommand>
    {
        private readonly IDiscussionsRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SendMessageHandler(
            IDiscussionsRepository repository,
            [FromKeyedServices(ModuleNames.Discussion)] IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<UnitResult<ErrorList>> Handle(SendMessageCommand command, CancellationToken cancellation)
        {
            var discussion = await _repository.GetById(command.discussionId, cancellation);
            if(discussion.IsFailure)
            {
                return discussion.Error.ToErrorList();
            }

            var message = Message.Create(
                command.message, 
                DateTime.UtcNow,
                command.userId, 
                discussion.Value.Id, 
                MessageId.NewMessageId);

            if(message.IsFailure)
            {
                return message.Error.ToErrorList();
            }

            var result = discussion.Value.SendComment(message.Value);
            if (result.IsFailure)
            {
                return result.Error.ToErrorList();
            }

            await _unitOfWork.SaveChanges(cancellation);

            return Result.Success<ErrorList>();
        }
    }
}
