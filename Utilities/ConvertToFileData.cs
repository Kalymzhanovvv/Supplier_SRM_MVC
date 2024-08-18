using Supplier_SRM_MVC.Entities;

namespace Supplier_SRM_MVC.Utilities
{
    public static class ConvertToFileData
    {
        public static async Task<FileData> ConvertFromIFormFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentNullException(nameof(file));
            }

            var filePath = Path.Combine("C:\\Users\\kalym\\OneDrive\\Desktop\\SRM.API files", file.FileName);

            var fileDirectory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(fileDirectory))
            {
                Directory.CreateDirectory(fileDirectory!);
            }
            
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileData = new FileData
            {
                FileName = file.FileName,
                Path = filePath,
                Size = file.Length,
                UploadDate = DateTime.Now,
            };
            return fileData;
        }
    }
}
