using Microsoft.EntityFrameworkCore;
using PetFamily.Accounts.Domain.DataModels;
using PetFamily.Application.AccountManagement.DataModels;
using PetFamily.Core.Dtos.Accounts;

namespace PetFamily.Accounts.Application
{
    public interface IAccountsReadDbContext
    {
        DbSet<AdminAccountDto> AdminAccounts { get; set; }
        DbSet<ParticipantAccountDto> ParticipantAccounts { get; set; }
        //DbSet<Permission> Permissions { get; set; }
        //DbSet<RefreshSession> RefreshSessions { get; set; }
        //DbSet<RolePermission> RolePermissions { get; set; }
        DbSet<RoleDto> Roles { get; set; }
        DbSet<UserDto> Users { get; set; }
        DbSet<VolunteerAccountDto> VolunteerAccounts { get; set; }
    }
}