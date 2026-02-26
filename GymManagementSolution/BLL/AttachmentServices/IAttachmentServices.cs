using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.AttachmentServices
{
    public interface IAttachmentServices
    {
        public string? Upload(string folderName, IFormFile file);

        public bool Delete(string folderPath, string fileName);
    }
}
