using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Models
{
    public class Chart
    {
        [Key]
        public string ChartId { get; set; } // Guid as a string
        public string ChartName { get; set; }
        public int DataYear { get; set; }
        public string HealthIndicator { get; set; }
        public string Counties { get; set; } // a string list of county FIPS codes. Ex: 1000,1001,1002,1003.
        public int? RegionId { get; set; }
        public string ChartDescription { get; set; }
        public DateTime CreationDateTime { get; set; } // Format 11/10/2020 08:30 pm
        public DateTime UpdatedDateTime { get; set; } // Format 11/10/2020 08:30 pm
        public string CreatedBy { get; set; } // Guid as a string
        public string UpdatedBy { get; set; } // Guid as a string
    }
}
