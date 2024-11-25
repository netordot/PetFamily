using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.CoreCore.Dtos.PetManagement
{
    public record SocialDto
    {
        public string Name { get; init; }
        public string Link { get; init; }

        public SocialDto(string name, string link)
        {
            Name = name;
            Link = link;
        }
    }
}
