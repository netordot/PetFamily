using Microsoft.EntityFrameworkCore;
using PetFamily.Core.Abstractions;
using PetFamily.Core.Dtos.Accounts;
using PetFamily.Core.Dtos.PetManagement;
using PetFamily.CoreCore.Dtos.PetManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Application.Queries.GetAccountById
{
    public class GetAccountByIdHandler : IQueryHandler<UserDto, GetAccountByIdQuery>
    {
        private readonly IAccountsReadDbContext _context;

        public GetAccountByIdHandler(IAccountsReadDbContext readDbContext)
        {
            _context = readDbContext;
        }
        public async Task<UserDto> Handle(GetAccountByIdQuery query, CancellationToken cancellation)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                //.ThenInclude(r => r.RolePermissions)
                .Include(u => u.AdminAccount)
                .Include(u => u.VolunteerAccount)
                .Include(u => u.ParticipantAccount)
                .FirstOrDefaultAsync(u => u.Id == query.AcocuntId, cancellation);

            if (user == null)
            {
                return null;
            }

            var roles = user.Roles == null ?
                []
                : user.Roles.Select(r => new RoleDto() { Id = r.Id, Name = r.Name });

            var socialNetwroks = user.SocialNetworks == null ?
                []
                : user.SocialNetworks.Select(s => new SocialDto(s.Name, s.Link));

            var adminAccount = user.AdminAccount != null ? new AdminAccountDto()
            {
                Id = user.AdminAccount.Id,
                FristName = user.AdminAccount.FristName,
                MiddleName = user.AdminAccount.MiddleName!,
                LastName = user.AdminAccount.LastName!,
                UserId = user.Id
            } : null;

            var volunteerAccount = user.VolunteerAccount != null ? new VolunteerAccountDto()
            {
                Id = user.VolunteerAccount.Id,
                UserId = user.Id,
                FirstName = user.VolunteerAccount.FirstName,
                MiddleName = user.VolunteerAccount.MiddleName!,
                LastName = user.VolunteerAccount.LastName!,
                Experience = user.VolunteerAccount.Experience,
                Requisites = user.VolunteerAccount.Requisites is null ?
                []
                : user.VolunteerAccount.Requisites.Select(r => new RequisiteDto(r.Description, r.Title)).ToList()
            } : null;


            var participantAccount = user.ParticipantAccount != null ? new ParticipantAccountDto()
            {
                Id = user.ParticipantAccount.Id,
                UserId = user.ParticipantAccount.Id,
                FristName = user.ParticipantAccount.FristName,
                MiddleName = user.ParticipantAccount.MiddleName!,
                LastName = user.ParticipantAccount.LastName!
            } : null;

            var userDto = new UserDto()
            {
                Id = user.Id,
                Roles = roles.ToList(),
                SocialNetworks = socialNetwroks.ToList(),
                AdminAccount = adminAccount,
                ParticipantAccount = participantAccount,
                VolunteerAccount = volunteerAccount,
            };


            return userDto;
        }
    }

}


