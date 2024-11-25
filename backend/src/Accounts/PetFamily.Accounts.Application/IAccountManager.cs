using PetFamily.Accounts.Domain.DataModels;
using PetFamily.Application.AccountManagement.DataModels;

namespace PetFamily.Accounts.Application
{
    public interface IAccountManager
    {
        public Task AddAdminAccount(AdminAccount adminAccount);
        public Task AddParticipantAccount(ParticipantAccount participantAccount);
    }
}