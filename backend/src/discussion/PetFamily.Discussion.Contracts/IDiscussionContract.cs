using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Contracts
{
    public interface IDiscussionContract
    {
        public Task<Result<Guid, ErrorList>> CreateDiscussion(Guid RelationId, Guid UserId, Guid AdminId);
        public Task<UnitResult<ErrorList>> CloseDiscussionById(Guid DiscussionId, Guid AdminId);
        public Task<UnitResult<ErrorList>> AddMessage(Guid DiscussionId, Guid UserId, string Message);
    }
}
