using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Infrastructure.Options
{
    public class AdminOptions
    {
        public static string ADMIN = "Admin";
        public string  Email { get; set; }
        public string  UserName { get; set; }
        public string  Password { get; set; }

    }
}
