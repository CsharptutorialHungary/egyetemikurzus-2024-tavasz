using System.Text.Json.Serialization;
namespace Filemanager.Model{

    public sealed record class FolderDef{

    [JsonPropertyName("name")]
    public string Name {get; init;}

    [JsonPropertyName("types")]

    public string[] Types {get; set;}

    public FolderDef(string name, string[] types){
        this.Name=name;
        this.Types=types;
    }
}
}