using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Dtos
{
    public class RequisiteDto
    {
        public string Title { get; init; }
        public string Description { get; init; }

        public RequisiteDto(string title, string description)
        {
            Title = title;
            Description = description; 
        }

    }
}
