using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace IF5W4R.Services
{
    public class FileService
    {
        public async Task<T> ReadFromJsonFileAsync<T>(string filePath)
        {
            using FileStream fileStream = File.OpenRead(filePath);
            return await JsonSerializer.DeserializeAsync<T>(fileStream);
        }
    }
}
