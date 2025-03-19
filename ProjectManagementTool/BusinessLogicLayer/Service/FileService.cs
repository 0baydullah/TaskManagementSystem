using BusinessLogicLayer.IService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;
        public FileService(IWebHostEnvironment env)
        {
            _env = env; 
        }
        public async Task<List<string>> UploadFile(List<IFormFile> files)
        {
            var fileUrls = new List<string>();

            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    string folder = Path.Combine(_env.WebRootPath, "files");
                    if (Directory.Exists(folder) == false)
                    {
                        Directory.CreateDirectory(folder);
                    }

                    string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(folder, fileName);
                    await file.CopyToAsync(new FileStream(filePath, FileMode.Create));
                    fileUrls.Add(fileName);
                }
            }

            return fileUrls;
        }
    }
}
