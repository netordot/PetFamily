using PetFamily.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PetFamily.Species.Application.Commands.DeleteSpecies
{
    public record DeleteSpeciesCommand(Guid id) : Core.Abstractions.ICommand;

}
