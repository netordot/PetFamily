﻿using PetFamily.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Volunteers.Application.Commands.Delete
{
    public record DeleteVolunteerCommand(Guid Id) : ICommand;
}
