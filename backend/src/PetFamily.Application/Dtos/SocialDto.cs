using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Dtos
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
