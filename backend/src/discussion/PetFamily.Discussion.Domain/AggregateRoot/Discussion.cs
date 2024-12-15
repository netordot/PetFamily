using CSharpFunctionalExtensions;
using PetFamily.Discussion.Domain.Entities;
using PetFamily.Discussion.Domain.Enums;
using PetFamily.Discussion.Domain.ValueObjects;
using PetFamily.SharedKernel.Id;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PetFamily.Discussion.Domain.AggregateRoot
{
    public class Discussion : SharedKernel.ValueObjects.Entity<DiscussionId>
    {
        private List<Message> _messages = [];
        public Guid RelationId { get; private set; }
        public DiscussionStatus Status { get; private set; }
        public Users Users { get; set; }
        public IReadOnlyList<Message> Messages => _messages;

        private Discussion(DiscussionId id) : base(id)
        {
        }

        private Discussion(
            DiscussionId discussionId,
            Guid relationId,
            Users users,
            DiscussionStatus status) : this(discussionId)
        {
            RelationId = relationId;
            Users = users;
            Status = status;
        }

        public static Discussion Create(DiscussionId id, Guid relationId, Users users)
        {
            return new Discussion(id, relationId, users, DiscussionStatus.Active);
        }

        public UnitResult<Error> SendComment(Message message)
        {
            if (Users.UsersExists(message.UserId) == false)
            {
                return Errors.General.ValueIsInvalid(nameof(message));
            }

            _messages.Add(message);

            return Result.Success<Error>();
        }

        public UnitResult<Error> DeleteComment(Guid messageId, Guid userId)
        {
            if (Users.UsersExists(userId) == false)
            {
                return Error.Conflict("access forbidden", "unable to delete unauthored messages");
            }

            var message = _messages.FirstOrDefault(m => m.Id.Value == messageId);
            if (message == null)
            {
                return Errors.General.NotFound(messageId);
            }

            if (message.UserId != userId)
            {
                return Error.Conflict("unowned.edit", "no rights for delete");
            }

            _messages.Remove(message);

            return Result.Success<Error>();
        }


        public UnitResult<Error> EditComment(Guid messageId, Guid userId, string text)
        {
            var comment = _messages.FirstOrDefault(m => m.Id.Value == messageId);
            if (Users.UsersExists(userId) == false)
            {
                return Error.Conflict("access forbidden", "unable to delete unauthored messages");
            }

            if (comment == null)
            {
                return Errors.General.NotFound();
            }

            if (comment.UserId != userId)
            {
                return Error.Conflict("unowned.edit", "no rights for edit");
            }

            var updateResult = comment.Edit(text);
            if (updateResult.IsFailure)
            {
                return updateResult.Error;
            }

            return Result.Success<Error>();
        }

        public void Close()
        {
            Status = DiscussionStatus.Inactive;
        }
    }
}