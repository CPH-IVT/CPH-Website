using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Models
{
    public class SavedChartCounties
    {
        public int Id { get; set; }
        public int ChartId { get; set; }
        public int CountyId { get; set; }
    }
}
