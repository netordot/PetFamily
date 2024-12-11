using CSharpFunctionalExtensions;
using FluentAssertions;
using PetFamily.SharedKernel.Id;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.VolunteerRequest.Domain.AggregateRoot;
using PetFamily.VolunteerRequest.Domain.Enums;
using PetFamily.VolunteerRequest.Domain.ValueObjects;

namespace PetFamily.Tests.VolunteerRequest.Domain
{
    public class ChangeRequestStatuses
    {
        [Fact]
        public void Approve_After_Submit()
        {
            // arrange
            var request = CreateVolunteerRequest();
            var adminId = Guid.NewGuid();
            var discussionId = Guid.NewGuid();
            // act
            request.Value.TakeOnReview(adminId, discussionId);
            request.Value.Approve();
            // assert

            request.Value.Status.Should().Be(VolunteerRequestStatus.Approved);
        }

        [Fact]
        public void Decline_After_Sumbit()
        {
            // arrange
            var request = CreateVolunteerRequest();
            var adminId = Guid.NewGuid();
            var discussionId = Guid.NewGuid();
            // act
            request.Value.TakeOnReview(adminId, discussionId);
            request.Value.Decline();
            // assert

            request.Value.Status.Should().Be(VolunteerRequestStatus.Rejected);
        }

        [Fact]
        public void Revision_After_Sumbit()
        {
            // arrange
            var request = CreateVolunteerRequest();
            var adminId = Guid.NewGuid();
            var discussionId = Guid.NewGuid();
            // act
            request.Value.TakeOnReview(adminId, discussionId);
            request.Value.SetOnRevision("unsafe");
            // assert

            request.Value.Status.Should().Be(VolunteerRequestStatus.RevisionRequired);
        }

        [Fact]
        public void Submit_Revision_Resend_Decline()
        {
            // arrange
            var request = CreateVolunteerRequest();
            var adminId = Guid.NewGuid();
            var discussionId = Guid.NewGuid();
            // act
            request.Value.TakeOnReview(adminId, discussionId);
            request.Value.SetOnRevision("unsafe");
            request.Value.SubmitAfterRevision();
            request.Value.Decline();
            // assert

            request.Value.Status.Should().Be(VolunteerRequestStatus.Rejected);
        }

        [Fact]
        public void Submit_Revision_Resend_Approve()
        {
            // arrange
            var request = CreateVolunteerRequest();
            var adminId = Guid.NewGuid();
            var discussionId = Guid.NewGuid();
            // act
            request.Value.TakeOnReview(adminId, discussionId);
            request.Value.SetOnRevision("unsafe");
            request.Value.SubmitAfterRevision();
            request.Value.Decline();
            // assert

            request.Value.Status.Should().Be(VolunteerRequestStatus.Rejected);
            request.IsFailure.Should().BeFalse();
        }

        // проверка на ошибки

        //[Fact]
        //public void NoSubmit_Approve_Error()
        //{
        //    // arrange
        //    var request = CreateVolunteerRequest();
        //    var adminId = Guid.NewGuid();
        //    var discussionId = Guid.NewGuid();
        //    // act
        //    request.Value.Approve();
        //    // assert

        //    request.IsFailure.Should().BeTrue();
        //}

        //[Fact]
        //public void NoSubmit_Decline_Error()
        //{
        //    // arrange
        //    var request = CreateVolunteerRequest();
        //    var adminId = Guid.NewGuid();
        //    var discussionId = Guid.NewGuid();
        //    // act
        //    request.Value.Decline();
        //    // assert

        //   request.Error.Should().NotBeNull();
        //}

        //[Fact]
        //public void NoSubmit_Revision_Error()
        //{
        //    // arrange
        //    var request = CreateVolunteerRequest();
        //    var adminId = Guid.NewGuid();
        //    var discussionId = Guid.NewGuid();
        //    // act
        //    request.Value.SetOnRevision("unsafe");
        //    // assert

        //    request.Error.Should().NotBeNull();
        //}

        //[Fact]
        //public void Submit_Approve_Decline_Error()
        //{
        //    // arrange
        //    var request = CreateVolunteerRequest();
        //    var adminId = Guid.NewGuid();
        //    var discussionId = Guid.NewGuid();
        //    // act
        //    request.Value.TakeOnReview(adminId, discussionId);
        //    request.Value.Decline();
        //    // assert

        //    request.Error.Should().NotBeNull();
        //}

        private Result<PetFamily.VolunteerRequest.Domain.AggregateRoot.VolunteerRequest, Error> CreateVolunteerRequest()
        {
            var id = VolunteerRequestId.NewVolunteerRequestId;
            var userId = Guid.NewGuid();
            var creationDate = DateTime.UtcNow;

            var phoneNumber = PhoneNumber.Create("222-11-22").Value;
            var email = Email.Create("hjdkj@mail.com").Value;
            var fullName = new FullName("jjjj", "sdsd", "dfdjfh");
            var requisites = new List<Requisite>()
            {
                new Requisite("rirlkw", "description")
            };

            var volunteerInfo = new VolunteerRequestInfo(fullName, 1, email, phoneNumber, "check", requisites);

            var volunteerRequest = PetFamily.VolunteerRequest.Domain.AggregateRoot.VolunteerRequest.Create
                (id, userId, volunteerInfo, creationDate);

            return volunteerRequest;

        }
    }
}