using Newtonsoft.Json;

namespace Our.Umbraco.RobotsTxtEditor.Models
{
    public class RobotsTxtEditorModel
    {
        [JsonProperty("fileContents")]
        public string FileContents { get; set; }

        [JsonProperty("fileExists")]
        public bool FileExists { get; set; }
    }
}