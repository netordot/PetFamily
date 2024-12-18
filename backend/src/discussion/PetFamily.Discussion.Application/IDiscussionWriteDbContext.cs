using Microsoft.EntityFrameworkCore;

namespace PetFamily.Discussion.Application
{
    public interface IDiscussionWriteDbContext
    {
        DbSet<Domain.AggregateRoot.Discussion> Discussions { get; set; }
    }
}