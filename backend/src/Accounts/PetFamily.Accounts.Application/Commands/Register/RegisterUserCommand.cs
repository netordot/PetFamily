using PetFamily.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PetFamily.Accounts.Application.Commands.Register
{
    public record RegisterUserCommand(string Email, string UserName, string Password) : Core.Abstractions.ICommand;
}
