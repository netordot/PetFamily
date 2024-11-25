using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos.PetManagement;
using PetFamily.SharedKernel.Other;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Application;
using PetFamily.Volunteers.Application.Querries.GetVolunteer;
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
