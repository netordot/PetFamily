using CSharpFunctionalExtensions;
using PetFamily.Discussion.Application.Commands.CloseDiscussion;
using PetFamily.Discussion.Application.Commands.CreateDiscussion;
using PetFamily.Discussion.Application.Commands.SendMessage;
using PetFamily.Discussion.Contracts;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Presentation
{
    public class DiscussionContract : IDiscussionContract
    {
        private readonly CreateDiscussionHandler _createDiscussionHandler;
        private readonly SendMessageHandler _sendMessageHandler;
        private readonly CloseDiscussionHandler _closeDiscussionHandler;

        public DiscussionContract(
            CreateDiscussionHandler createDiscussionHandler,
            SendMessageHandler sendMessageHandler,
            CloseDiscussionHandler closeDiscussionHandler)
        {
            _createDiscussionHandler = createDiscussionHandler;
            _sendMessageHandler = sendMessageHandler;
            _closeDiscussionHandler = closeDiscussionHandler;
        }
        public async Task<UnitResult<ErrorList>> AddMessage(Guid DiscussionId, Guid UserId, string Message)
        {
            var command = new SendMessageCommand(UserId, DiscussionId, Message);
            var result = await _sendMessageHandler.Handle(command, CancellationToken.None);
            if (result.IsFailure)
            {
                return result.Error;
            }

            return result;
        }

        public async Task<UnitResult<ErrorList>> CloseDiscussionById(Guid DiscussionId, Guid AdminId)
        {
            var command = new CloseDiscussionCommand(AdminId, DiscussionId);

            var result = await _closeDiscussionHandler.Handle(command, CancellationToken.None);
            if (result.IsFailure)
            {
                return result.Error;
            }

            return result;
        }

        public async Task<Result<Guid, ErrorList>> CreateDiscussion(Guid RelationId, Guid UserId, Guid AdminId)
        {
            var command = new CreateDiscussionCommand(RelationId, AdminId, UserId);

            var result = await _createDiscussionHandler.Handle(command, CancellationToken.None);
            if (result.IsFailure)
            {
                return result.Error;
            }

            return result;
        }
    }
}
