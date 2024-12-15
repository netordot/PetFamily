using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Core.Dtos.Discussion
{
    public class DiscussionDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid AdminId { get; set; }
        public Guid RelationId { get; set; }
        public List<MessageDto> Messages { get; set; }
        public DiscussionStatusDto Status { get; set; }
    }
}
