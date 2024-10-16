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

        public IReadOnlyList<Domain.PetPhoto> Photos { get; }

        public PetPhotos(IEnumerable<Domain.PetPhoto> petPhotos)
        {
            Photos = petPhotos.ToList();
        }

    }
}
