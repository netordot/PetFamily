using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.Id;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Domain.Entities
{
    public class Message : SharedKernel.ValueObjects.Entity<MessageId>
    {
        public string Text { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool IsEdited { get; private set; } = false;
        public Guid UserId { get; private set; }
        public DiscussionId DiscussionId { get; private set; }

        private Message(MessageId id) : base(id)
        {
        }

        private Message(
            string text,
            DateTime createdAt,
            Guid userId,
            DiscussionId discusisonId,
            MessageId id) : base(id)
        {
            Text = text;
            CreatedAt = createdAt;
            UserId = userId;
            DiscussionId = discusisonId;
        }

        public static Result<Message, Error> Create(
            string text,
            DateTime createdAt,
            Guid userId,
            DiscussionId discusisonId,
            MessageId id)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return Errors.General.ValueIsRequired("message");
            }

            return new Message(text, createdAt, userId, discusisonId, id);
        }

        public UnitResult<Error> Edit(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return Errors.General.ValueIsInvalid("message");
            }

            Text = text;
            CreatedAt = DateTime.UtcNow;
            IsEdited = true;

            return Result.Success<Error>();
        }
    }
}