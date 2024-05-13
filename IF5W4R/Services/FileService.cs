using System.Text.Json;

namespace IF5W4R.Services
{
    public class FileService : IFileService
    {
        public async Task<T> ReadFromJsonFileAsync<T>(string filePath)
        {
            using FileStream fileStream = File.OpenRead(filePath);
            return await JsonSerializer.DeserializeAsync<T>(fileStream);
        }

        public async Task WriteToJsonFileAsync<T>(string filePath, T data)
        {
            using FileStream fileStream = File.Create(filePath);
            await JsonSerializer.SerializeAsync<T>(fileStream, data);
        }
    }
}
