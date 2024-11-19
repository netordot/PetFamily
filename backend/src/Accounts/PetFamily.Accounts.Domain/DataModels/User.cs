using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.AccountManagement.DataModels
{
    public class User : IdentityUser<Guid>
    {
        public List<SocialNetwork> SocialNetworks { get; set; } = [];
    }
}
