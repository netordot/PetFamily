using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Core.Dtos.VolunteerRequest
{
    public class UserRestrictionDto
    {
        public Guid Id { get; set; }
        public Guid DiscussionId { get;  set; }
        public DateTime BannedUntil { get;  set; }
        public string BanReason { get;  set; }
    }
}
