using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Abstractions;
using PetFamily.Application.Database;
using PetFamily.Application.Dtos;
using PetFamily.Application.Models;
using PetFamily.Domain.Shared.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.PetManagement.Queries.GetVolunteer
{
    public class GetVolunteerHandler : ICommandHandler<VolunteerDto, GetVolunteerCommand>
    {
        private readonly IReadDbContext _readContext;

        public GetVolunteerHandler(IReadDbContext context)
        {
            _readContext = context;
        }

        public async Task<Result<VolunteerDto, ErrorList>> Handle (GetVolunteerCommand command, CancellationToken cancellation)
        {
            // добавить валидацию
            var volunteer = await _readContext.Volunteers.FirstOrDefaultAsync(v => v.Id == command.id, cancellation);
            if (volunteer == null)
            {
                var error = Errors.General.NotFound(command.id);
                return error.ToErrorList();
            }

            return volunteer;
        }

    }
}
