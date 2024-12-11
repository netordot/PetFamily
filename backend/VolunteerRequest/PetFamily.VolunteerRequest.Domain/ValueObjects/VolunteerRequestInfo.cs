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
    }
}
