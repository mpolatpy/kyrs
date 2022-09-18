using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetAdoption.ApplicationUtils
{
    public class PetSearchParams
    {
        public string Type { get; set; }
        public string Gender { get; set; }
        public string ZipCode { get; set; }
    }
}