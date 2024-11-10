using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.PetManagement.Queries.GetPet
{
    public class GetPetHandler : IQueryHandler<PetDto, GetPetQuery>
    {
        private readonly IReadDbContext _readDbContext;

        public GetPetHandler(IReadDbContext readDbContext)
        {
            _readDbContext = readDbContext;
        }
        public async Task<PetDto> Handle(GetPetQuery query, CancellationToken cancellation)
        {
            var pet = await _readDbContext.Pets.FirstOrDefaultAsync(p => p.Id == query.PetId);
            
            return pet;
        }
    }
}
