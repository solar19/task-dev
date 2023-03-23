using System.IO;
using System.Text.Json;

namespace dev.task._780
{
    class Program
    {
        private const string FolderPath = "./images/";
        private const string JsonFileInfo = "./fileInfos.json";
        private const string SourceImagesPath = "links.txt";

        static async Task Main(string[] args)
        {
            var imageUrls = ReadImageUrlsFromFile();
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }

            var fileInfos = await DownloadImages(imageUrls);
            WriteFileInfo(fileInfos);

            Console.WriteLine("File info has been saved successfully!");
        }

        private static IEnumerable<string> ReadImageUrlsFromFile()
        {
            return File.ReadLines(SourceImagesPath).Where(l => !string.IsNullOrWhiteSpace(l));
        }

        private static async Task<List<FileInfo>> DownloadImages(IEnumerable<string> imageUrls)
        {
            var fileInfos = new List<FileInfo>();
            using var httpClient = new HttpClient();
            foreach (var url in imageUrls)
            {
                var fileName = Path.GetFileName(url);
                var response = await httpClient.GetAsync(url);

                using var fileStream = new FileStream(Path.Combine(FolderPath, fileName), FileMode.Create);
                await response.Content.CopyToAsync(fileStream);

                fileInfos.Add(new FileInfo
                {
                    LocalName = Path.GetFileNameWithoutExtension(url),
                    URL = url,
                    FileSize = response.Content.Headers.ContentLength,
                    FileExtension = response.Content.Headers.ContentType!.ToString(),
                    DownloadDate = DateTime.UtcNow
                });
            }
            return fileInfos;
        }

        private static void WriteFileInfo(List<FileInfo> fileInfos)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(fileInfos, options);
            File.WriteAllText(JsonFileInfo, json);
        }
    }
}