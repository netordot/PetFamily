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
        private Discussion(DiscussionId discussionId) : base(discussionId) { }
        public Guid RelationId { get; private set; }
        public DiscussionStatus Status { get; private set; } = DiscussionStatus.Active;
        public Users Users { get; private set; }
        public IReadOnlyList<Message> Messages => _messages;
        private List<Message> _messages { get; set; } = new List<Message>();

        private Discussion(Users users, Guid relationId, DiscussionId discussionId)
            : base(discussionId)
        {
            Users = users;
            RelationId = relationId;
        }

        public static Result<Discussion, Error> Create
            (Users users,
            Guid relationId,
            DiscussionId discussionId)
        {
            if (users == null)
            {
                return Errors.General.ValueIsRequired("users");
            }

            if (users.UserId == Guid.Empty || users.AdminId == Guid.Empty)
            {
                return Errors.General.ValueIsInvalid("id is required");
            }

            return new Discussion(users, relationId, discussionId);
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
            if(message == null)
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
            if(updateResult.IsFailure)
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
