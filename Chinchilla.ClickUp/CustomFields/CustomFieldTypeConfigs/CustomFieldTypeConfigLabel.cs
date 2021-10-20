using Newtonsoft.Json;
using System.Collections.Generic;

namespace Chinchilla.ClickUp.CustomFields
{

    public class CustomFieldTypeConfigLabel : CustomFieldTypeConfig
    {
        [JsonProperty("options")]
        public List<CustomFieldTypeConfigOptionLabel> Options { get; set; }
    }
}
