using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IService
{
    public interface IFileService
    {
        public Task<List<string>> UploadFile(List<IFormFile> file);
    }
}
