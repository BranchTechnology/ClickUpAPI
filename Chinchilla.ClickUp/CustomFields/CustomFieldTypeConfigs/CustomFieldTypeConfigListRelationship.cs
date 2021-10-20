using Newtonsoft.Json;
using System.Collections.Generic;

namespace Chinchilla.ClickUp.CustomFields
{

    public class CustomFieldTypeConfigListRelationship : CustomFieldTypeConfig
    {
        [JsonProperty("fields")]
        public List<CustomFieldTypeConfigField> Fields { get; set; }
    }
}
