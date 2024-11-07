using PetFamily.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PetFamily.Application.Species.GetAllBreeds
{
    public record GetAllBreedsCommand(Guid Speciesid, Guid BreedId) : Abstractions.ICommand;

}
