using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.SharedKernel.Id
{
    public record VolunteerRequestId
    {
        public Guid Value { get; }

        private VolunteerRequestId(Guid value)
        {
            Value = value;
        }

        private VolunteerRequestId()
        {

        }

        public static VolunteerRequestId NewVolunteerRequestId => new(Guid.NewGuid());
        public static VolunteerRequestId Empty => new(Guid.Empty);
        public static VolunteerRequestId Create(Guid id) => new(id);
        public static implicit operator Guid(VolunteerRequestId volunteerRequestId)
        {
            ArgumentNullException.ThrowIfNull(volunteerRequestId);
            return volunteerRequestId.Value;
        }
    }
}
