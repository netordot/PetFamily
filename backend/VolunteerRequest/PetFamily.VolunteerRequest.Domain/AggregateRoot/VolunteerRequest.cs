using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.Id;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.VolunteerRequest.Domain.Enums;
using PetFamily.VolunteerRequest.Domain.ValueObjects;
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
        public VolunteerRequestInfo VolunteerInfo { get; private set; }
        public VolunteerRequestStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private VolunteerRequest(VolunteerRequestId id) : base(id)
        {
            
        }

        private VolunteerRequest(
            VolunteerRequestId id,
            Guid userId,
            VolunteerRequestInfo volunteerInfo,
            DateTime createdAt) : base(id)
        {
            UserId = userId;
            RejectionComment = string.Empty;
            VolunteerInfo = volunteerInfo;
            CreatedAt = createdAt;
            Status = VolunteerRequestStatus.Waiting;
        }

        public static Result<VolunteerRequest, Error> Create(
            VolunteerRequestId id,
            Guid userId,
            VolunteerRequestInfo volunteerInfo,
            DateTime createdAt)
        {

            return new VolunteerRequest(
                id,
                userId,
                volunteerInfo,
                createdAt);
        }

        // взять на рассмотрение
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

        // отправить на доработку
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

            Status = VolunteerRequestStatus.RevisionRequired;
            RejectionComment = rejectionComment;

            return Result.Success<Error>();
        }
        // отклонить 
        public UnitResult<Error> Decline()
        {
            if (Status != VolunteerRequestStatus.Submited)
            {
                return Errors.General.ValueIsInvalid("status");
            }

            Status = VolunteerRequestStatus.Rejected;

            return Result.Success<Error>();
        }
        // принять  
        public UnitResult<Error> Approve()
        {
            if (Status != VolunteerRequestStatus.Submited)
            {
                return Errors.General.ValueIsInvalid("status");
            }

            Status = VolunteerRequestStatus.Approved;

            return Result.Success<Error>();
        }

        // взять на рассмотрение после доработки
        public UnitResult<Error> SubmitAfterRevision()
        {
            if (Status != VolunteerRequestStatus.RevisionRequired)
            {
                return Errors.General.ValueIsInvalid("status");
            }

            Status = VolunteerRequestStatus.Submited;

            return Result.Success<Error>();
        }


    }
}
