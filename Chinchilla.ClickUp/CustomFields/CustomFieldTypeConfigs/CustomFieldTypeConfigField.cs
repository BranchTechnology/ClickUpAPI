using Newtonsoft.Json;

namespace Chinchilla.ClickUp.CustomFields
{
    public class CustomFieldTypeConfigField
    {
        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }
    }
}
