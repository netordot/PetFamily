using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.Id;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Domain.AggregateRoot
{
    public class UserRestriction : SharedKernel.ValueObjects.Entity<UserRestrictionId>
    {
        public UserRestriction(UserRestrictionId id) : base(id)
        {
        }
        public const int DEFAULT_BAN_DAYS = 7;

        public Guid DiscussionId { get; private set; }
        public DateTime BannedUntil { get; private set; }
        public string BanReason { get; private set; }

        public UserRestriction(Guid discussionId, DateTime bannedUntil, string banReason, UserRestrictionId userRestrictionId) : base
            (userRestrictionId)
        {
            DiscussionId = discussionId;
            BanReason = banReason;  
            BannedUntil = bannedUntil;
        }

        public static Result<UserRestriction, Error> Create(Guid discussionId, int bannedDays, string banReason, UserRestrictionId userRestrictionId)
        {
            if(bannedDays <= 0)
            {
                return Errors.General.ValueIsInvalid("banned days");
            }

            var bannedUntil = DateTime.UtcNow.AddDays(bannedDays);
            return new UserRestriction(discussionId, bannedUntil, banReason, userRestrictionId);
        }
        public UnitResult<Error> EndBanManually()
        {
            BannedUntil = DateTime.UtcNow;
            return UnitResult.Success<Error>();
        }

        public int GetDaysLeft()
        {
            var result =  BannedUntil - DateTime.UtcNow;
            
            if(result.Days <=0)
            {
                return 0;
            }

            return result.Days;
        }

        public UnitResult<Error> ExtendBan(int days)
        {
            if(days <= 0)
            {
                return Errors.General.ValueIsInvalid("days of ban");
            }

            BannedUntil.AddDays(days);

            return UnitResult.Success<Error>();
        }

        public bool IsActive => BannedUntil <= DateTime.UtcNow;


    }
}
