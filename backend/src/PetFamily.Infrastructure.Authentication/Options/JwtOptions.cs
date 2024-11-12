using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Infrastructure.Authentication.Options
{
    public class JwtOptions
    {
        public const string JWT = nameof(JWT);
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public string ExpiredMinutesTime { get; set; }
    }
}
