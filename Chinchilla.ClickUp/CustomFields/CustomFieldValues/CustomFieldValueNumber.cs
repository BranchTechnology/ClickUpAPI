using Newtonsoft.Json;

namespace Chinchilla.ClickUp.CustomFields
{
    public class CustomFieldValueNumber : CustomFieldValue
    {
        [JsonProperty("value")]
        public double Value { get; set; }
    }
}
