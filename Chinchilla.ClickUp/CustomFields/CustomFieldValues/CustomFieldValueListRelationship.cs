using Newtonsoft.Json;
using System.Collections.Generic;

namespace Chinchilla.ClickUp.CustomFields
{
    public class CustomFieldValueListRelationship : CustomFieldValue
    {
        [JsonProperty("value")]
        public List<CustomFieldValueListRelationshipValue> Value { get; set; }
    }
}
