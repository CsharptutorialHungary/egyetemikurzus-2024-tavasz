using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

using BKYZSA.Domain;

namespace BKYZSA.Infrastructure
{
    internal class DialogueSerializer
    {
        public async static Task SerializeToJson(Stream target, Dialogue dialogue)
        {
            await JsonSerializer.SerializeAsync(target, dialogue, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin,
                                                   UnicodeRanges.Latin1Supplement,
                                                   UnicodeRanges.LatinExtendedA)
            });
        }

        public static Dialogue? DeserializeFromJson(Stream source)
        {
            var messages = JsonSerializer.Deserialize<Dialogue>(source, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin,
                                                   UnicodeRanges.Latin1Supplement,
                                                   UnicodeRanges.LatinExtendedA)
            });

            return messages;
        }
        
    }
}
