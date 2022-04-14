using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Models
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string FIPS { get; set; } // a string list of county FIPS codes. Ex: 1000,1001,1002,1003.
    }
}
