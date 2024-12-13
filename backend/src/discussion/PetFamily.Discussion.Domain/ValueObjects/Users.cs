using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Discussion.Domain.ValueObjects
{
    public record Users
    {
        public Guid UserId { get; private set; }
        public Guid AdminId { get; private set; }

        public Users(Guid userId, Guid adminId)
        {
            UserId = userId;
            AdminId = adminId;
        }

        public bool UsersExists(Guid id)
            => UserId == id || AdminId == id;
    }
}
