using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Core.Messaging
{
    public interface IMessageQueue<TMessage>
    {
        Task WriteAsync(TMessage paths, CancellationToken cancellation);

        Task<TMessage> ReadAsync(CancellationToken cancellation);
    }
}
