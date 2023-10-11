
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Chinchilla.ClickUp.Responses.Model
{

    /// <summary>
    /// Model object of List information response
    /// </summary>
    public class ResponseModelListCustomField
        : Helpers.IResponse
    {

        /// <summary>
        /// Id of the List
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Name of the List
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Type of the List
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
