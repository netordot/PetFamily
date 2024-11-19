using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Core.Dtos.PetManagement
{
    public class PetPhotoDto
    {
        public string PathToStorage { get; init; }
        public bool IsMain { get; set; }
    }
}
