 using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Domain.ValueObjects
{
    public record VolunteerRequestInfo
    {
        public FullName FullName { get;}
        public int Experience { get;}
        public Email Email { get;}
        public PhoneNumber PhoneNumber { get;}
        public string Description { get;} 
        public List<Requisite> Requisites { get;}

        public VolunteerRequestInfo(
            FullName fullName,
            int experience,
            Email email,
            PhoneNumber phoneNumber,
            string description,
            List<Requisite> requisites
            )
        {
            FullName = fullName;
            Experience = experience;
            Email = email;
            PhoneNumber = phoneNumber;
            Description = description;
            Requisites = requisites;
        }

        private VolunteerRequestInfo()
        {
            
        }
    }
}
