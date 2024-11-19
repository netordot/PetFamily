using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Core.Dtos.PetManagement
{
    public class FileDto
    {
        public string PathToStorage { get; set; }

        public bool IsMain { get; set; }
    }
}
