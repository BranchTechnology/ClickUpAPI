using Newtonsoft.Json;
using System.Collections.Generic;

namespace Chinchilla.ClickUp.CustomFields
{

    public class CustomFieldTypeConfigDropDown : CustomFieldTypeConfig
    {
        [JsonProperty("default")]
        public int Default { get; set; }

        [JsonProperty("options")]
        public List<CustomFieldTypeConfigOptionDropDown> Options { get; set; }
    }
}
