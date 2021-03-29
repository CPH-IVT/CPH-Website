using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Models
{
    public class SavedChartKeywords
    {
        public int Id { get; set; }
        public int KeywordId { get; set; }
        public int SavedChartId { get; set; }
    }
}
