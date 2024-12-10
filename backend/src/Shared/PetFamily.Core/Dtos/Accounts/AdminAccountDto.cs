using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Core.Dtos.Accounts
{
    public class AdminAccountDto

    {
        public static string ADMIN = "Admin";
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserDto User { get; set; }
        public string FristName { get; init; } = string.Empty;
        public string MiddleName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
    }
}
