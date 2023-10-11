using Newtonsoft.Json;

namespace Chinchilla.ClickUp.CustomFields
{
    public class CustomFieldValueUrl : CustomFieldValue
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
