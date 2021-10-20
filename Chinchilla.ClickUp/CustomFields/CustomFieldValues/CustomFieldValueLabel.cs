using Newtonsoft.Json;
using System.Collections.Generic;

namespace Chinchilla.ClickUp.CustomFields
{
    public class CustomFieldValueLabel : CustomFieldValue
    {
        [JsonProperty("value")]
        public List<string> Value { get; set; }
    }
}
