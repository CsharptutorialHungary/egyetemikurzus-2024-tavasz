using System.Text.Json.Serialization;
namespace Filemanager.Model{

    public sealed record class FolderDef{

    [JsonPropertyName("name")]
    private string Name {get; init;}

    [JsonPropertyName("types")]

    private string[] Types {get; init;}
}
}