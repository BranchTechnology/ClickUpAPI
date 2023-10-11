using Newtonsoft.Json;

namespace Chinchilla.ClickUp.CustomFields
{
    public class CustomFieldTypeConfigOption
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("orderindex")]
        public int OrderIndex { get; set; }
    }
}
