using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XWG8AW.Domain
{
    public sealed record class UserJson
    {

        [JsonPropertyName("userName")]
        public string UserName { get; init; }

        [JsonPropertyName("score")]
        public int Score { get; init; }


    }
}
