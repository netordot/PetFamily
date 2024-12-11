using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.Id;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.VolunteerRequest.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Domain.AggregateRoot
{
    public class VolunteerRequest : SharedKernel.ValueObjects.Entity<VolunteerRequestId>
    {
        public Guid DiscussionId { get; private set; }
        public Guid AdminId { get; private set; }
        public Guid UserId { get; private set; }
        public string RejectionComment { get; private set; } = string.Empty;
        public string VolunteerInfo { get; private set; } = string.Empty;
        public VolunteerRequestStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private VolunteerRequest(VolunteerRequestId id) : base(id)
        {
            
        }


        private VolunteerRequest(
            VolunteerRequestId id,
            Guid userId,
            string volunteerInfo,
            DateTime createdAt) : base(id)
        {
            UserId = userId;
            RejectionComment = string.Empty;
            VolunteerInfo = volunteerInfo;
            CreatedAt = createdAt;
            Status = VolunteerRequestStatus.Waiting;
        }

        public static Result<VolunteerRequest, Error> Createt(
            VolunteerRequestId id,
            Guid userId,
            string volunteerInfo,
            DateTime createdAt)
        {

            return new VolunteerRequest(
                id,
                userId,
                volunteerInfo,
                createdAt);
        }

        public UnitResult<Error> TakeOnReview(Guid adminId, Guid discussionId)
        {
            if (Status != VolunteerRequestStatus.Waiting)
            {
                return Errors.General.ValueIsInvalid("status");
            }

            Status = VolunteerRequestStatus.Submited;
            DiscussionId = discussionId; 
            AdminId = adminId;

            return Result.Success<Error>();
        }

        public UnitResult<Error> SetOnRevision(string rejectionComment)
        {
            if (Status != VolunteerRequestStatus.Submited)
            {
                return Errors.General.AlreadyExists();
            }

            if(string.IsNullOrWhiteSpace(rejectionComment))
            {
                return Errors.General.ValueIsInvalid("rejection comment");
            }

            Status = VolunteerRequestStatus.ResivionRequired;
            RejectionComment = rejectionComment;

            return Result.Success<Error>();
        }
        public UnitResult<Error> Decline()
        {
            if (Status != VolunteerRequestStatus.Submited)
            {
                return Errors.General.ValueIsInvalid("status");
            }

            Status = VolunteerRequestStatus.Rejected;

            return Result.Success<Error>();
        }
        public UnitResult<Error> Approve()
        {
            if (Status != VolunteerRequestStatus.Submited)
            {
                return Errors.General.ValueIsInvalid("status");
            }

            Status = VolunteerRequestStatus.Approved;

            return Result.Success<Error>();
        }


    }
}
