namespace IF5W4R.Services
{
    public interface IFileService
    {
        Task<T> ReadFromJsonFileAsync<T>(string filePath);
        Task WriteToJsonFileAsync<T>(string filePath, T data);
    }
}
