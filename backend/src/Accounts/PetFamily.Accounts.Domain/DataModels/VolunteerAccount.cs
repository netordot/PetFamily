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

        private List<Requisite> _requisites { get; set; } = [];

        public List<Requisite> Requisites  => _requisites;

        public VolunteerAccount(Guid userId, Guid id, User user, int experience, FullName fullName, List<Requisite> requisites)
        {
            User = user;
            UserId = userId;
            Id = id;
            Experience = experience;
            FullName = fullName;
            _requisites = requisites;
        }

        public void UpdateRequisites(IEnumerable<Requisite> requisites)
        {
            _requisites = requisites.ToList();  
        }
    }
}
