﻿using PetFamily.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.PetManagement.Queries.GetVolunteer
{
    public record GetVolunteerCommand(Guid id) : ICommand;
    
}