using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Dtos.Discussion;

namespace PetFamily.Discussion.Application
{
    public interface IDiscussionReadDbContext
    {
        DbSet<DiscussionDto> Discussions { get; set; }
        DbSet<MessageDto> Messages { get; set; }
    }
}