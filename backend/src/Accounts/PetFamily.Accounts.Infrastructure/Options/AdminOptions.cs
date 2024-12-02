﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Infrastructure.Options
{
    public class AdminOptions
    {
        public static string ADMIN = "Admin";
        public string  Email { get; set; } = string.Empty;  
        public string  UserName { get; set; } = string.Empty ;
        public string Password { get; set; } = string.Empty;

    }
}
