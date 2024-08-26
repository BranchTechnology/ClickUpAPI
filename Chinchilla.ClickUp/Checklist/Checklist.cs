using Newtonsoft.Json;
using System.Collections.Generic;

namespace Chinchilla.ClickUp
{
    public class ChecklistItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("resolved")]
        public bool Resolved { get; set; }

        [JsonProperty("assignee")]
        public string Assignee{ get; set; }

    }

    public class Checklist
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("items")]
        public List<ChecklistItem> Items { get; set; }
    }

}
