using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.VolunteerRequest.Infrastructure.Data
{
    public class VolunteerRequestReadDbContext : DbContext
    {
        private const string DATABASE = nameof(Database);
    }
}
