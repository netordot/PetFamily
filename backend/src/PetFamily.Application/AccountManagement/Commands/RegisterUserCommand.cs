using PetFamily.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PetFamily.Application.AccountManagement.Commands
{
    public record RegisterUserCommand(string Email, string UserName, string Password) : Abstractions.ICommand;
}
