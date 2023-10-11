using Newtonsoft.Json;

namespace Chinchilla.ClickUp.CustomFields
{
    public class CustomFieldTypeConfigOptionLabel : CustomFieldTypeConfigOption
    {
        [JsonProperty("label")]
        public string Label { get; set; }
    }
}
