using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Domain.DataModels
{
    public record JwtTokenResult(string AccessToken, Guid RefreshToken);
    
}
