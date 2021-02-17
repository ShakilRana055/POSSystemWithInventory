using Microsoft.AspNetCore.Hosting;
using POSSystemWithInventory.RepositoryPattern.Interfaces.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.RepositoryPattern.Repositories.UnitOfWork
{
    public class ImageProcessing:IImageProcessing
    {
        private IWebHostEnvironment _environment;
        public ImageProcessing(IWebHostEnvironment hostingEnvironment)
        {
            _environment = hostingEnvironment;
        }

        public string GetImagePath(string fileName, string folderName)
        {
            string path = _environment.WebRootPath + "\\" + "images" + "\\" + folderName + "\\";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path + fileName;
        }

        public string GetImagePathForDb(string imagePath)
        {
            string webRootFolder = _environment.WebRootPath;
            imagePath = imagePath.Replace(webRootFolder, "");
            imagePath = imagePath.Replace(@"\", "/");
            return imagePath;
        }
    }
}
