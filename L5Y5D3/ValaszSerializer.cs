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
        public static async Task SerializeToJson(Stream target, List<Valasz> instance)
        {

            await JsonSerializer.SerializeAsync(target, instance, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}
