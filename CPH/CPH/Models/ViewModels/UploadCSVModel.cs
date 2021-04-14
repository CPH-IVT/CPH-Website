using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Models.ViewModels
{
    public class UploadCSVModel
    {
        public string UserIdentity { get; set; }
        public IFormFile File { get; set; }
        public DateTime UploadeDate { get; set; }

    }
}
