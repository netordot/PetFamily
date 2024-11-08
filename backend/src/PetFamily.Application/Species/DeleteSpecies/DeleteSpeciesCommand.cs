using PetFamily.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PetFamily.Application.Species.DeleteSpecies
{
    public record DeleteSpeciesCommand(Guid id) : Abstractions.ICommand;
    
}
