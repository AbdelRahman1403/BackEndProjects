using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.AttachmentServices
{
    public class AttachmentServices : IAttachmentServices
    {
        private readonly string[] extensions = { ".jpg", ".jpeg", ".png" };
        private readonly long maxFileSize = 5 * 1024 * 1024; // 5 MB

        public string? Upload(string folderName, IFormFile file)
        {
            if(file is null || folderName is null)
            {
                return null;
            }
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!extensions.Contains(fileExtension))
            {
                return null;
            }

            if(file.Length > maxFileSize || file.Length <= 0)
            {
                return null;
            }

            var folderpath = Path.Combine(Directory.GetCurrentDirectory() , "wwwroot","images" , folderName);
            if(!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }
            var fileName = Guid.NewGuid().ToString() + fileExtension;
            var filePath = Path.Combine(folderpath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(stream);
            return filePath;
        }
        public bool Delete(string folderPath, string fileName)
        {
            if(string.IsNullOrEmpty(folderPath) || string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory() , "wwwroot" , "images" , folderPath , fileName);
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }
    }
}
