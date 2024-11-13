using PetFamily.Application.AccountManagement.Commands;

namespace PetFamily.API.Contracts.Authorization
{
    public record RegisterUserRequest(string Email, string UserName, string Password);
}
