using PetFamily.Application.AccountManagement.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Domain.DataModels
{
    public class RefreshSession
    {
        public Guid Id { get; init; }
        public Guid RefreshToken { get; init; }
        public Guid UserId { get; init; }
        public User User { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime ExpiresAt { get; init; }
    }
}
