using PetFamily.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.AccountManagement.Commands.LogIn
{
    public record LogInUserCommand(string Email, string Password) : ICommand;
    
}
