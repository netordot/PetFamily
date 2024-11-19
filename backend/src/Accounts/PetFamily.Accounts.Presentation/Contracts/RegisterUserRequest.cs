using PetFamily.Application.AccountManagement.Commands;

namespace PetFamily.Accounts.Presentation.Contracts
{
    public record RegisterUserRequest(string Email, string UserName, string Password);
}
