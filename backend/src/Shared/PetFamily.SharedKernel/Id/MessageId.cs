using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.SharedKernel.Id
{
    public class MessageId
    {
        public Guid Value { get; }

        private MessageId(Guid value)
        {
            Value = value;
        }

        private MessageId()
        {

        }

        public static MessageId NewMessageId => new(Guid.NewGuid());
        public static MessageId Empty => new(Guid.Empty);
        public static MessageId Create(Guid id) => new(id);
        public static implicit operator Guid(MessageId messageId)
        {
            ArgumentNullException.ThrowIfNull(messageId);
            return messageId.Value;
        }
    }
}
