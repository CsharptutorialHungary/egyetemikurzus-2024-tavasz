using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace L5Y5D3
{
    internal class ValaszSerializer
    {
        public void SerializeToJson(Stream target, List<Valasz> instance)
        {
            JsonSerializer.SerializeAsync(target, instance, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}
