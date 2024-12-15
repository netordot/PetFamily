using CSharpFunctionalExtensions;
using FluentAssertions;
using PetFamily.Discussion.Domain.Entities;
using PetFamily.Discussion.Domain.Enums;
using PetFamily.Discussion.Domain.ValueObjects;
using PetFamily.SharedKernel.Id;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Tests.Discussion.Domain
{
    public class DiscussionDomainTests
    {
        [Fact]
        public void Open_Close()
        {
            // arrange
            var discussion = InitializeDiscussion().Value;
            // act

            discussion.Close();
            // assert
            discussion.Status.Should().Be(DiscussionStatus.Inactive);
        }

        [Fact]
        public void Add_Messages()
        {
            // arrange
            var discussion = InitializeDiscussion().Value;
            var message = Message.Create("testwt message",
                DateTime.UtcNow,
                discussion.Users.AdminId,
                discussion.Id,
                MessageId.NewMessageId);

            var message1 = Message.Create("testwt message1",
             DateTime.UtcNow,
             discussion.Users.UserId,
             discussion.Id,
             MessageId.NewMessageId);

            var message2 = Message.Create("testwt message2",
             DateTime.UtcNow,
             discussion.Users.AdminId,
             discussion.Id,
             MessageId.NewMessageId);
            // act
            discussion.SendComment(message.Value);
            discussion.SendComment(message1.Value);
            discussion.SendComment(message2.Value);

            // assert
            discussion.Messages.Should().HaveCount(3);
        }

        [Fact]
        public void Add_Edit_Messages()
        {
            // arrange
            var discussion = InitializeDiscussion().Value;
            var message = Message.Create("testwt message",
                DateTime.UtcNow,
                discussion.Users.AdminId,
                discussion.Id,
                MessageId.NewMessageId);

            var message1 = Message.Create("testwt message1",
             DateTime.UtcNow,
             discussion.Users.UserId,
             discussion.Id,
             MessageId.NewMessageId);

            var message2 = Message.Create("testwt message2",
             DateTime.UtcNow,
             discussion.Users.AdminId,
             discussion.Id,
             MessageId.NewMessageId);
            // act
            discussion.SendComment(message.Value);
            discussion.SendComment(message1.Value);
            discussion.SendComment(message2.Value);

            discussion.EditComment(message1.Value.Id.Value, message1.Value.UserId, "new text");

            // assert
            discussion.Messages.Should().HaveCount(3);
            discussion.Messages[1].Text.Should().Be("new text");
        }

        [Fact]
        public void Add_Edit_Delete_Messages()
        {
            // arrange
            var discussion = InitializeDiscussion().Value;
            var message = Message.Create("testwt message",
                DateTime.UtcNow,
                discussion.Users.AdminId,
                discussion.Id,
                MessageId.NewMessageId);

            var message1 = Message.Create("testwt message1",
             DateTime.UtcNow,
             discussion.Users.UserId,
             discussion.Id,
             MessageId.NewMessageId);

            var message2 = Message.Create("testwt message2",
             DateTime.UtcNow,
             discussion.Users.AdminId,
             discussion.Id,
             MessageId.NewMessageId);

            var message3 = Message.Create("testwt message3",
             DateTime.UtcNow,
             discussion.Users.AdminId,
             discussion.Id,
             MessageId.NewMessageId);
            // act
            discussion.SendComment(message.Value);
            discussion.SendComment(message1.Value);
            discussion.SendComment(message2.Value);
            discussion.SendComment(message3.Value);

            discussion.EditComment(message1.Value.Id.Value, message1.Value.UserId, "new text");
            discussion.DeleteComment(message3.Value.Id.Value, message3.Value.UserId);

            // assert
            discussion.Messages.Should().HaveCount(3);
            discussion.Messages[1].Text.Should().Be("new text");
        }

        private static Result<PetFamily.Discussion.Domain.AggregateRoot.Discussion, Error> InitializeDiscussion()
        {
            var users = new Users(Guid.NewGuid(), Guid.NewGuid());

            var relationId = Guid.NewGuid();
            var discussionId = DiscussionId.NewDiscussionId;

            var discussion = PetFamily.Discussion.Domain.AggregateRoot.Discussion
                .Create(DiscussionId.NewDiscussionId, relationId, users);

            return discussion;
        }
    }
}