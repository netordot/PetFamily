//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Storage;
//using PetFamily.Domain.Pet.Species;
//using PetFamily.Domain.Volunteer;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PetFamily.Application.Database
//{
//    public interface IApplicationDbContext
//    {
//        DbSet<Volunteer> Volunteers { get; }
//        DbSet<Domain.Pet.Species.Species> Species { get; } 
//        Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellation);
//        Task<int> SaveChanges(CancellationToken cancellation);
//    }
//}
