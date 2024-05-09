using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XWG8AW.Domain
{
    public sealed record class Question
    {

        [JsonPropertyName("name")]
        public required string Name { get; init; }
        
        [JsonPropertyName("answer_a")]
        public required string Answer_A { get; init; }
        
        [JsonPropertyName("answer_b")]
        public required string Answer_B { get; init; }
        
        [JsonPropertyName("answer_c")]
        public required string Answer_C { get; init; }
        
        [JsonPropertyName("answer_d")]
        public required string Answer_D { get; init; }
        
        [JsonPropertyName("correct")]
        public required string Correct { get; init; }

    }
}
