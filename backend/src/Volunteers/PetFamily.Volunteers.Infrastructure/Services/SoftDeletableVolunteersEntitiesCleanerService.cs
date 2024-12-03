using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PetFamily.SharedKernel.Id;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Volunteers.Infrastructure.Services
{
    public class SoftDeletableVolunteersEntitiesCleanerService : ISoftDeletableVolunteersEntitiesCleanerService
    {
        private readonly WriteDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly string SOFT_DELETE_DAYS = "SoftDeleteDays";

        public SoftDeletableVolunteersEntitiesCleanerService(
            WriteDbContext context,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task Process(CancellationToken cancellation)
        {
            
            await DeleteVolunteers(cancellation);
           // await DeletePets(cancellation);
        }

        public async Task DeleteVolunteers(CancellationToken cancellation)
        {
            var volunteers = await _context
                 .Volunteers
                 .Where(v => v.IsDeleted == true)
                 .ToListAsync();

            if(volunteers.Count == 0 || volunteers == null)
            {
                return;
            }

            foreach (var volunteer in volunteers)
            {
                if (volunteer.DeletionDate != null
                    && volunteer.DeletionDate >= DateTime.UtcNow.AddDays(double.Parse(_configuration.GetSection(SOFT_DELETE_DAYS).Value!)))
                {
                    _context.Remove(volunteer);
                }
            }

            await _context.SaveChangesAsync(cancellation);
        }

        //public async Task DeletePets(CancellationToken cancellation)
        //{
        //    var pets = await _context
        //            .Set<SoftDeletableEntity<PetId>>()
        //            .Where(v => v.IsDeleted == true)
        //            .ToListAsync();

        //    foreach (var pet in pets)
        //    {
        //        if (pet.DeletionDate != null
        //            && pet.DeletionDate >= DateTime.UtcNow.AddDays(double.Parse(_configuration.GetSection(SOFT_DELETE_DAYS).Value!)))
        //        {
        //            _context.Remove(pet);
        //        }
        //    }

        //    await _context.SaveChangesAsync(cancellation);
        //}
    }
}
