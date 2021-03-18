using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Models.ViewModels
{
    public class CreateChart
    {
        public List<int> Years { get; set; }
        public List<string> DataPoints { get; set; }
        public List<string> Couties { get; set; }

    }
}
