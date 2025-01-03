﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Core.Dtos.PetManagement
{
    public record RequisiteDto
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
