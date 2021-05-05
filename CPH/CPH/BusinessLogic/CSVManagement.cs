using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.BusinessLogic
{
    public class CSVManagement
    {
        private string _csvPath;
        public string CSVPath { get { return _csvPath; } }
        public string Folder { get { return _folder; } }
        private string _folder;
        public readonly string FileName;
        public readonly IFormFile File;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CSVManagement(IWebHostEnvironment hostEnvironment, IFormFile file)
        {
 
            File = file;
            FileName = file.Name;
            _hostEnvironment = hostEnvironment;

        }

        public async Task UploadCSVAsync()
        {
            try
            {
                BuildCSVUploadFolder();
                CreateDirectoryForCSV();
                BuildCSVUploadPath();
                await CopyCsvToDirectoryAsync();

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"The file was not found: '{e}'");
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine($"The directory was not found: '{e}'");
            }
            catch (IOException e)
            {
                Console.WriteLine($"The file could not be opened: '{e}'");
            }

        }

        private void BuildCSVUploadFolder()
        {
            _folder = _hostEnvironment.WebRootPath + "\\uploads\\";
        }

        private void CreateDirectoryForCSV()
        {
            if(Folder != null)
            {
                Directory.CreateDirectory(_folder);
            }
        }

        private void BuildCSVUploadPath()
        {
            if(_folder != null && FileName != null)
            {
                _csvPath = Path.Combine(_folder, FileName);
            }
        }

        private async Task CopyCsvToDirectoryAsync()
        {
            using(var stream = System.IO.File.Create(_csvPath))
            {
                await File.CopyToAsync(stream);
            }
        }

    }
}
