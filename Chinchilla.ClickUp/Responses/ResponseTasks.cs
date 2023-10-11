using Newtonsoft.Json;
using Chinchilla.ClickUp.Responses.Model;
using System.Collections.Generic;

namespace Chinchilla.ClickUp.Responses
{

	/// <summary>
	/// Response object of the method GetTasks()
	/// </summary>
	public class ResponseTasks
		: Helpers.IResponse
	{
		/// <summary>
		/// List of Model Task with information of Tasks received
		/// </summary>
		[JsonProperty("tasks")]
		public List<ResponseModelTask> Tasks { get; set; }
        [JsonProperty("last_page")]
        public bool LastPage { get; set; }

    }
}