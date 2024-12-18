using PetFamily.Application.Extensions;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos.Discussion;
using PetFamily.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Application.Queries.GetDiscussionWithMessages
{
    public class GetDiscussionWithMessagesHandler : IQueryHandler<List<MessageDto>, GetDiscussionWithMessagesQuery>
    {
        private readonly IDiscussionReadDbContext _readDbContext;

        public GetDiscussionWithMessagesHandler(IDiscussionReadDbContext discussionReadDbContext)
        {
            _readDbContext = discussionReadDbContext;
        }

        public async Task<List<MessageDto>> Handle(GetDiscussionWithMessagesQuery query, CancellationToken cancellation)
        {
            var messageQuery = _readDbContext.Messages.Where(m => m.DiscussionId == query.DiscussionId);
            var result = messageQuery.ToList();

            if (result.Any(m => m.UserId != query.userId))
            {
                result = [];
            }

            return result;
        }
    }
}
