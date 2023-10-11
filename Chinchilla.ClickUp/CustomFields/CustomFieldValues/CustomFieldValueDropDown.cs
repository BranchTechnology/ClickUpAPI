using Newtonsoft.Json;

namespace Chinchilla.ClickUp.CustomFields
{
    public class CustomFieldValueDropDown : CustomFieldValue
    {
        [JsonProperty("value")]
        public int Value { get; set; }
    }
}
