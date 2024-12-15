using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Core.Dtos.Discussion
{
    public class MessageDto
    {
        public string Text { get;  set; }
        public DateTime CreatedAt { get;  set; }
        public bool IsEdited { get;  set; }
        public Guid UserId { get;  set; }
        public Guid DiscussionId { get; set; }
        public Guid Id { get; set; }
    }
}
