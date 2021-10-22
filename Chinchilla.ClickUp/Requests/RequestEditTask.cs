using Newtonsoft.Json;
using Chinchilla.ClickUp.Enums;
using Chinchilla.ClickUp.Requests.Model;
using System;
using Chinchilla.ClickUp.Helpers;

namespace Chinchilla.ClickUp.Requests
{
	/// <summary>
	/// Request object for method EditTask()
	/// </summary>
	public class RequestEditTask
	{
		#region Attributes

		/// <summary>
		/// Name of the task
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		/// Content of the task
		/// </summary>
		[JsonProperty("content")]
		public string Content { get; set; }

		/// <summary>
		/// Content of the task in MarkDown
		/// </summary>
		[JsonProperty("markdown_content")]
		public string MarkDownContent { get; set; }

		/// <summary>
		/// List of users id added or removed to the task
		/// </summary>
		[JsonProperty("assignees")]
		public RequestModelSupportAssignees Assignees { get; set; }

		/// <summary>
		/// Status of the Task
		/// </summary>
		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("archived")]
		public bool IsArchived { get; set; }

		/// <summary>
		/// Priority of the Task
		/// </summary>
		[JsonProperty("priority")]
		public TaskPriority? Priority { get; set; }

		/// <summary>
		/// Due Date of the task
		/// </summary>
		[JsonProperty("due_date")]
		[JsonConverter(typeof(JsonConverterDateTimeMilliseconds))]
		public DateTime? DueDate { get; set; }

		#endregion


		//#region Constructor

            /// Removed Constructor because not all RequestEditTask should edit the name or any other property
		///// <summary>
		///// Constructor of RequestEditTask
		///// </summary>
		///// <param name="name"></param>
		//public RequestEditTask(string name)
		//{
		//	Name = name;
		//}

		//#endregion


		#region Public Methods

		/// <summary>
		/// Validation method of data
		/// </summary>
		public void ValidateData()
		{
			if (string.IsNullOrEmpty(Name))
			{
				throw new ArgumentNullException("Name");
			}
		}

		#endregion
	}
}