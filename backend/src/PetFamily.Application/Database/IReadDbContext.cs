﻿using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Database
{
    public interface IReadDbContext
    {
        DbSet<SpeciesDto> Species { get; }
        DbSet<VolunteerDto> Volunteers { get; }
    }

}