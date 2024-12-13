using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.SharedKernel.Id
{
    public record DiscussionId
    {
        public Guid Value { get; }

        private DiscussionId(Guid value)
        {
            Value = value;
        }

        private DiscussionId()
        {

        }

        public static DiscussionId NewDiscussionId => new(Guid.NewGuid());
        public static DiscussionId Empty => new(Guid.Empty);
        public static DiscussionId Create(Guid id) => new(id);
        public static implicit operator Guid(DiscussionId discussionId)
        {
            ArgumentNullException.ThrowIfNull(discussionId);
            return discussionId.Value;
        }
    }
}
