using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Dtos
{
    public class PetPhotoDto
    {
        public string PathToStorage { get; set; }

        public bool IsMain { get; set; }

        //public PetPhotoDto(string path, bool isMain)
        //{
        //    PathToStorage = path;
        //    IsMain = isMain;
        //}
    }
}
