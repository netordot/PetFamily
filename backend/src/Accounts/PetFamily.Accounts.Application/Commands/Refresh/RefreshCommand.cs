using PetFamily.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Accounts.Application.Commands.Refresh
{
    public record RefreshCommand(Guid refreshToken) : ICommand;
  
}
