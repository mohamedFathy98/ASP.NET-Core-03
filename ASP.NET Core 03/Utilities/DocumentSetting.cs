namespace ASP.NET_Core_03.Utilities
{
    public class DocumentSetting
    {
        public static string UoloadFile(IFormFile file, string folderName)
        {
            // Create FolderPath
            //  string folderPath = Directory.GetCurrentDirectory() + @"\wwwroot\Files";
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", folderName);

            //Create Unique Name for File
            string fileName = $"{Guid.NewGuid()}-{file.FileName}";
            //Create File Path
            string filePath = Path.Combine(folderPath, fileName);
            //FileStream to save
            using var stream = new FileStream(filePath, FileMode.Create);

            //copy file to filestrem
            file.CopyTo(stream);
            return fileName;

        }

        public static void DeleteFile(string folderName, string fileName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", folderName, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
