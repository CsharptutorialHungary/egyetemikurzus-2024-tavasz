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
    internal class MessageEntrySerializer
    {
        public void SerializeToJson(Stream target, List<MessageEntry> messageEntries)
        {
            JsonSerializer.Serialize(target, messageEntries, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin,
                                                   UnicodeRanges.Latin1Supplement,
                                                   UnicodeRanges.LatinExtendedA)
            });
        }
    }
}
