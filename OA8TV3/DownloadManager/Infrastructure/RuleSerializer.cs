using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

using DownloadManager.Domain;

namespace DownloadManager.Infrastructure
{
    internal class RuleSerializer
    {
        private static readonly string? ProjectDir =
            Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName;

        private static readonly JsonSerializerOptions WriteOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true
        };

        private static readonly JsonSerializerOptions ReadOptions = new JsonSerializerOptions
        {
            AllowTrailingCommas = true, NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        public void SerializeToJson(AbstractRule[] rules)
        {
            Debug.Assert(ProjectDir != null, nameof(ProjectDir) + " != null");
            using (var stream = File.Create(Path.Combine(ProjectDir, "rules.json")))
            {
                JsonSerializer.Serialize(stream, rules, WriteOptions);
            }
        }

        public async Task<AbstractRule[]> DeserializeFromJson()
        {
            Debug.Assert(ProjectDir != null, nameof(ProjectDir) + " != null");
            try
            {
                await using (var stream = File.OpenRead(Path.Combine(ProjectDir, "rules.json")))
                {
                    var rules = await JsonSerializer.DeserializeAsync<AbstractRule[]>(stream, ReadOptions);
                    if (rules is null)
                    {
                        Trace.WriteLine("Json deszerializáció sikertelen");
                        return [];
                    }
                    return rules;
                }
            }
            catch (FileNotFoundException e)
            {
                return [];
            }
        }
    }
}