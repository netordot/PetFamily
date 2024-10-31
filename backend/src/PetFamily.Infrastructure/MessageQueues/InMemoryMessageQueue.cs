using PetFamily.Application.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.MessageQueues
{
  
    public class InMemoryMessageQueue<TMessage> : IMessageQueue<TMessage>
    {
        private readonly Channel<TMessage> _channel = Channel.CreateUnbounded<TMessage> ();

        public async Task WriteAsync(TMessage paths, CancellationToken cancellation)
        {
            await _channel.Writer.WriteAsync(paths, cancellation);
        }

        public async Task<TMessage> ReadAsync(CancellationToken cancellation)
        {
            return await _channel.Reader.ReadAsync( cancellation);
        }
    }
}
