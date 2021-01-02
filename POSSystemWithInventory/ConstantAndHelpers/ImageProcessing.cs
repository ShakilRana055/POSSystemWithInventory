using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.ConstantAndHelpers
{
    public class ImageProcessing
    {
        private IWebHostEnvironment _environment;
        public ImageProcessing(IWebHostEnvironment hostingEnvironment)
        {
            _environment = hostingEnvironment;
        }
        
        public string GetImagePath(string fileName, string parentFolderName, string folderName)
        {
            string path = _environment.WebRootPath + "\\" + "images" + "\\" + parentFolderName + "\\" + folderName + "\\";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path + fileName;
        }

        private string GetImagePathForDb(string imagePath)
        {
            string webRootFolder = _environment.WebRootPath;
            imagePath = imagePath.Replace(webRootFolder, "");
            imagePath = imagePath.Replace(@"\", "/");
            return imagePath;
        }
    }
}
