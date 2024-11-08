using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helper
{
    public static class DocumentSettings
    {
        public static string UploadFileAsync(IFormFile file, string folderName)
        {
            //string folderPath = Directory.GetCurrentDirectory() + @"\wwwroot\Files";
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Images", folderName);// create folder path

            string fileName = $"{Guid.NewGuid()}-{file.FileName}"; // create file name

            string filePath = Path.Combine(folderPath, fileName);// create file path

            using var stream = new FileStream(filePath, FileMode.Create);//create file to save file

             file.CopyToAsync(stream); //copy file to fileStream

            return fileName;
        }

        public static void DeleteFile(string foldeName, string fileName)
        {

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", foldeName, fileName);

            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);


        }
    }
}
