using PetFamily.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.AccountManagement.DataModels
{
    public class VolunteerAccount
    {
        public static string VOLUNTEER = "Volunteer";
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public User User { get; set; }
        public int Experience { get; set; }
        // TODO: после разделения на read write db Context сделать конфигурацию через конвертер
        public FullName FullName { get; set; }

        private List<Requisite> _requisites = [];
        private List<SocialNetwork> _socialNetworks = [];

        public void UpdateSocails(IEnumerable<SocialNetwork> socials)
        {
            _socialNetworks = socials.ToList();
        }

        public void UpdateRequisites(IEnumerable<Requisite> requisites)
        {
            _requisites = requisites.ToList();  
        }
    }
}
