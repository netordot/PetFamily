using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.SharedKernel.Id
{
    public record UserRestrictionId
    {
        public Guid Value { get; }

        private UserRestrictionId(Guid value)
        {
            Value = value;
        }

        private UserRestrictionId()
        {

        }

        public static UserRestrictionId NewUserRestrictionId => new(Guid.NewGuid());
        public static UserRestrictionId Empty => new(Guid.Empty);
        public static UserRestrictionId Create(Guid id) => new(id);
        public static implicit operator Guid(UserRestrictionId userRestrictionId)
        {
            ArgumentNullException.ThrowIfNull(userRestrictionId);
            return userRestrictionId.Value;
        }
    }
}
