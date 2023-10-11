using Newtonsoft.Json;

namespace Chinchilla.ClickUp.CustomFields
{
    public class CustomFieldTypeConfigOptionDropDown : CustomFieldTypeConfigOption
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
