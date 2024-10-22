using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Domain.Pet.PetPhoto
{
    public class PetPhotos
    {
        private PetPhotos()
        {
            
        }

        public IReadOnlyList<PetPhoto> Photos { get; }

        public PetPhotos(IEnumerable<PetPhoto> petPhotos)
        {
            Photos = petPhotos.ToList();
        }

    }
}
