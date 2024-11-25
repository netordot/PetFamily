using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.AccountManagement.DataModels
{
    public class AdminAccount
    {
        public static string ADMIN = "Admin";
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User  User { get; set; }
        public FullName FullName { get; set; }
    }
}
