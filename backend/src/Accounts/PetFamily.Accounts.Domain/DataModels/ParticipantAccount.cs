using PetFamily.Application.AccountManagement.DataModels;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Domain.DataModels
{
    public class ParticipantAccount
    {
        public static string PARTICIPANT = "Participant";
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public FullName FullName { get; set; }
    }
}
