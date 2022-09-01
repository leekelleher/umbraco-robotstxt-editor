using Newtonsoft.Json;
using System.Collections.Generic;

namespace Our.Umbraco.RobotsTxtEditor.Models
{
    public class RobotsTxtResponseModel
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("errorMessages")]
        public List<Pair> ErrorMessages { get; set; }
    }
}
