using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using PetFamily.Accounts.Domain.DataModels;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.AccountManagement.DataModels
{
    public class User : IdentityUser<Guid>
    {
        private User()
        {
            
        }
        private User(string email,
            List<Role> roles, 
            string userName)
        {
            Roles = roles;
            UserName = userName;
            Email = email; 
        }
        public List<SocialNetwork> SocialNetworks { get; set; } = [];
        public List<Role> Roles { get; set; }
        public AdminAccount? AdminAccount { get; set; }
        public VolunteerAccount? VolunteerAccount { get; set; }
        public ParticipantAccount? ParticipantAccount { get; set; }

        public static Result<User,Error> CreateParticipant(string email, Role role, string userName )
        {
            if(role.Name!= ParticipantAccount.PARTICIPANT)
            {
                return Errors.General.ValueIsInvalid("role");
            }

            return new User(email, [role], userName);
        }
        public static Result<User, Error> CreateVolunteer(string email, Role role, string userName)
        {
            if (role.Name != VolunteerAccount.VOLUNTEER)
            {
                return Errors.General.ValueIsInvalid("role");
            }

            return new User(email, [role], userName);
        }

        public static Result<User, Error> CreateAdmin(string email, Role role, string userName)
        {
            if (role.Name != AdminAccount.ADMIN)
            {
                return Errors.General.ValueIsInvalid("role");
            }

            return new User(email, [role], userName);
        }

        public void UpdateSocialNetworks(IEnumerable<SocialNetwork> socialNetworks)
        {
            SocialNetworks = socialNetworks.ToList();
        }



    }
}
