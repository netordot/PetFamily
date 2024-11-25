namespace PetFamily.Accounts.Contracts
{
    public interface IAccountsContract
    {
        Task<bool> CheckUserPermission(Guid userId, string permissionCode);
    }
}