using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Dtos.PetManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Volunteers.Application
{
    public interface IReadDbContext
    {
        DbSet<VolunteerDto> Volunteers { get; }
        public DbSet<PetDto> Pets { get; }
    }

}
