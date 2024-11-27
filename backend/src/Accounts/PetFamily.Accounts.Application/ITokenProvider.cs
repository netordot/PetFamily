using PetFamily.Application.AccountManagement.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Application
{
    public interface ITokenProvider
    {
        public string GenerateAccessToken(User user);
        public Task<Guid> GenerateRefreshToken(User user, CancellationToken cancellation);
    }
}
